using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Octokit;
using Octokit.Internal;
partial class Build {
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
        .OnlyWhenDynamic(() => Parameters.NugetApiKey is not null)
        .OnlyWhenDynamic(() => Parameters.ShouldPublishNugetPackages)
        .DependsOn(PackDemoApp)
        .DependsOn(PublishNugetPackages)
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
            catch (Exception) {
                // ignored
            }

            var newRelease = new NewRelease(releaseVersion.ToString()) {
                Name = tagName,
                GenerateReleaseNotes = true
            };
            var release = await GitHubTasks.GitHubClient.Repository.Release.Create(owner, name, newRelease);
            var nugetArtifacts = NugetRoot.GetFiles("*.nupkg").ToImmutableArray();
            var artifacts = nugetArtifacts.Concat(DemoDir.GetFiles());
            foreach (var artifactPath in artifacts) {
                await using var fileStream = File.OpenRead(artifactPath);
                var releaseAssetUpload = new ReleaseAssetUpload(artifactPath.Name, "application/octet-stream", fileStream, null);
                await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, releaseAssetUpload);
            }

            var nuget = NuGet.Protocol.Core.Types.Repository.Factory.GetCoreV3(Parameters.NugetFeedUrl);
            foreach (var nugetArtifact in nugetArtifacts) {
                var packageName = nugetArtifact.Name.Split(Parameters.Version)[0];
                await HideOutdatedPackages(nuget, releaseVersion, packageName);
            }
            return;

            async Task HideOutdatedPackages(SourceRepository sourceRepository, NuGetVersion nuGetVersion, string packageName) {
                var resource = await sourceRepository.GetResourceAsync<PackageMetadataResource>();
                var parametersNugetPackages = await resource.GetMetadataAsync(
                    packageName,
                    true,
                    false,
                    new SourceCacheContext(),
                    NugetLogger.Instance,
                    CancellationToken.None);

                var outdatedVersions = parametersNugetPackages
                    .Where(metadata => metadata.Identity.HasVersion)
                    .Where(metadata => metadata.Identity.Version.IsPrerelease)
                    .Where(metadata => metadata.Identity.Version < nuGetVersion);
                foreach (var outdatedVersion in outdatedVersions) {
                    var packageUpdateResource = await sourceRepository.GetResourceAsync<PackageUpdateResource>();
                    await packageUpdateResource.Delete(packageName, outdatedVersion.Identity.ToString(),
                        _ => Parameters.NugetApiKey, _ => true, false, NugetLogger.Instance);
                }
            }
        });
}