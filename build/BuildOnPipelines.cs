using System;
using System.IO;
using System.Linq;
using System.Threading;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Octokit;
using Octokit.Internal;
using Serilog;
partial class Build {
    Target FetchNuget => _ => _
        .Unlisted()
        .OnlyWhenDynamic(() => Parameters.ShouldPublishNugetPackages)
        .Executes(async () => {
            var logger = NugetLogger.Instance;
            var cancellationToken = CancellationToken.None;
            var cache = new SourceCacheContext();
            var repository = NuGet.Protocol.Core.Types.Repository.Factory.GetCoreV3(Parameters.NugetFeedUrl);
            var resource = await repository.GetResourceAsync<PackageMetadataResource>();
            var parametersNugetPackages = await resource.GetMetadataAsync(
                "Material.Avalonia",
                true,
                false,
                cache,
                logger,
                cancellationToken);

            var packages = parametersNugetPackages as IPackageSearchMetadata[] ?? parametersNugetPackages.ToArray();
            Parameters.NugetPackages = packages;

            Log.Information("Fetched {PackagesCount} package versions from nuget", packages.Length);
        });

    Target AppendNightlyVersionIfNeeded => _ => _
        .Unlisted()
        .DependsOn(FetchNuget)
        .Before(CreateIntermediateNugetPackages)
        .OnlyWhenDynamic(() => Parameters.ShouldPublishNugetPackages)
        .Executes(() => {
            var isVersionPublished = Parameters.NugetPackages
                .Where(metadata => metadata.Identity.HasVersion)
                .Any(metadata => metadata.Identity.Version.ToString() == Parameters.Version);

            Parameters.IsReleaseToPublish = !isVersionPublished;
            if (isVersionPublished) {
                Log.Information("Found already published version {Version}. Appending .{NightlyNumber}-nightly tag", Parameters.Version, Parameters.NightlyHeight);
                Parameters.Version += $".{Parameters.NightlyHeight}-nightly";
            }
        });

    Target PublishNugetPackages => _ => _
        .OnlyWhenDynamic(() => Parameters.NugetApiKey is not null)
        .OnlyWhenDynamic(() => Parameters.ShouldPublishNugetPackages)
        .DependsOn(PackNugetPackages)
        .Executes(() => {
            DotNetTasks.DotNetNuGetPush(s => s
                .SetSource(Parameters.NugetFeedUrl)
                .SetApiKey(Parameters.NugetApiKey)
                .SetTargetPath(NugetRoot / "*.nupkg")
                .EnableSkipDuplicate());
        });

    Target PublishRelease => _ => _
        .Unlisted()
        .OnlyWhenDynamic(() => Parameters.IsRunningOnGitHubActions)
        .DependsOn(FetchNuget, PackDemoApp)
        .Triggers(PublishNugetPackages)
        .Executes(async () => {
            var releaseVersion = NuGetVersion.Parse(GitHubActions.Instance.RefName.TrimStart('v'));
            var tagName = $"v{releaseVersion}";
            Parameters.Version = releaseVersion.ToString();

            var (owner, name) = (Repository.GetGitHubOwner(), Repository.GetGitHubName());
            var credentials = new Credentials(GitHubActions.Instance.Token);
            GitHubTasks.GitHubClient = new GitHubClient(
                new ProductHeaderValue(nameof(NukeBuild)),
                new InMemoryCredentialStore(credentials));

            try {
                var oldRelease = await GitHubTasks.GitHubClient.Repository.Release.Get(owner, name, tagName);
                await GitHubTasks.GitHubClient.Repository.Release.Delete(owner, name, oldRelease.Id);
            }
            catch (Exception _) {
                // ignored
            }

            var newRelease = new NewRelease(releaseVersion.ToString()) {
                Name = tagName,
                GenerateReleaseNotes = true
            };
            var release = await GitHubTasks.GitHubClient.Repository.Release.Create(owner, name, newRelease);
            var artifacts = NugetRoot.GetFiles("*.nupkg")
                .Concat(DemoDir.GetFiles());
            foreach (var artifactPath in artifacts) {
                await using var fileStream = File.OpenRead(artifactPath);
                var releaseAssetUpload = new ReleaseAssetUpload(artifactPath.Name, "application/octet-stream", fileStream, null);
                await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, releaseAssetUpload);
            }

            var outdatedVersions = Parameters.NugetPackages
                .Where(metadata => metadata.Identity.HasVersion)
                .Where(metadata => metadata.Identity.Version.IsPrerelease)
                .Where(metadata => metadata.Identity.Version < releaseVersion);
            foreach (var outdatedVersion in outdatedVersions) {
                var packageUpdateResource = await NuGet.Protocol.Core.Types.Repository.Factory.GetCoreV3(Parameters.NugetFeedUrl)
                    .GetResourceAsync<PackageUpdateResource>();
                await packageUpdateResource.Delete("Material.Avalonia", outdatedVersion.Identity.ToString(),
                    _ => Parameters.NugetApiKey, _ => true, false, NugetLogger.Instance);
            }
        });
}