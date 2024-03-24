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
using Serilog;

[GitHubActions("main", GitHubActionsImage.UbuntuLatest, AutoGenerate = false,
    OnPushBranches = ["master", "release/*"],
    InvokedTargets = [nameof(PublishNugetPackages)],
    ImportSecrets = [nameof(NuGetApiKey)])]
[GitHubActions("release", GitHubActionsImage.UbuntuLatest, AutoGenerate = false,
    OnPushTags = ["*"],
    WritePermissions = [GitHubActionsPermissions.Contents],
    InvokedTargets = [nameof(PublishRelease)],
    ImportSecrets = [nameof(NuGetApiKey)],
    EnableGitHubToken = true)]
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
        .DependsOn(PublishNugetPackages)
        .Executes(async () => {
            var tagName = $"v{Parameters.Version}";

            var (owner, name) = (Repository.GetGitHubOwner(), Repository.GetGitHubName());
            var credentials = new Credentials(GitHubActions.Instance.Token);
            GitHubTasks.GitHubClient = new GitHubClient(
                new ProductHeaderValue(nameof(NukeBuild)),
                new InMemoryCredentialStore(credentials));

            try {
                Log.Information("Deleting old release {TagName}", tagName);
                var oldRelease = await GitHubTasks.GitHubClient.Repository.Release.Get(owner, name, tagName);
                await GitHubTasks.GitHubClient.Repository.Release.Delete(owner, name, oldRelease.Id);
            }
            catch (Exception) {
                // ignored
            }

            Log.Information("Creating new release {TagName}", tagName);
            var newRelease = new NewRelease(tagName) {
                Name = tagName,
                GenerateReleaseNotes = true
            };
            var release = await GitHubTasks.GitHubClient.Repository.Release.Create(owner, name, newRelease);
            var nugetArtifacts = NugetRoot.GetFiles("*.nupkg").ToImmutableArray();
            foreach (var artifactPath in nugetArtifacts) {
                Log.Information("Uploading new release {TagName} asset: {AssetName}", tagName, artifactPath.Name);
                await using var fileStream = File.OpenRead(artifactPath);
                var releaseAssetUpload = new ReleaseAssetUpload(artifactPath.Name, "application/octet-stream", fileStream, null);
                await GitHubTasks.GitHubClient.Repository.Release.UploadAsset(release, releaseAssetUpload);
            }

            var nuget = NuGet.Protocol.Core.Types.Repository.Factory.GetCoreV3(Parameters.NugetFeedUrl);
            foreach (var nugetArtifact in nugetArtifacts) {
                var packageName = nugetArtifact.Name.Split(Parameters.Version.ToString())[0].TrimEnd('.');
                await HideOutdatedPackages(nuget, Parameters.Version, packageName);
            }
            return;

            async Task HideOutdatedPackages(SourceRepository sourceRepository, NuGetVersion nuGetVersion, string packageName) {
                Log.Information("Retrieving nightly packages version for {PackageName} to hide", packageName);
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
                    .Where(metadata => metadata.Identity.Version.IsNightly())
                    .Where(metadata => metadata.Identity.Version < nuGetVersion);
                foreach (var outdatedVersion in outdatedVersions) {
                    Log.Information("Hiding previous nightly version {Version}", outdatedVersion.Identity.Version.ToString());
                    var packageUpdateResource = await sourceRepository.GetResourceAsync<PackageUpdateResource>();
                    await packageUpdateResource.Delete(packageName, outdatedVersion.Identity.Version.ToString(),
                        _ => Parameters.NugetApiKey, _ => true, false, NugetLogger.Instance);
                }
                
                Log.Information("All previous nightly version for {PackageName} was hidden", packageName);
            }
        });
}