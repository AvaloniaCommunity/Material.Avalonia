using System;
using System.Linq;
using System.Threading;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;

[GitHubActions("main", GitHubActionsImage.UbuntuLatest, AutoGenerate = true,
    OnPushBranches = new[] { "master" },
    InvokedTargets = new[] { nameof(PublishNugetPackages) },
    ImportSecrets = new[] { nameof(NuGetApiKey) })]
partial class Build : NukeBuild {
    [Solution] readonly Solution Solution = null!;
    BuildParameters Parameters { get; set; } = null!;

    AbsolutePath ArtifactsDir => RootDirectory / "artifacts";
    AbsolutePath NugetIntermediateRoot => ArtifactsDir / "build-intermediate" / "nuget";
    AbsolutePath NugetRoot => ArtifactsDir / "nuget";

    Target Clean => _ => _
        .Executes(() => {
            ArtifactsDir.DeleteDirectory();
        });

    Target FetchNuget => _ => _
        .OnlyWhenDynamic(() => Parameters.ShouldPublishNugetPackages)
        .Executes(async () => {
            var logger = NullLogger.Instance;
            var cancellationToken = CancellationToken.None;
            var cache = new SourceCacheContext();
            var repository = Repository.Factory.GetCoreV3(Parameters.NugetFeedUrl);
            var resource = await repository.GetResourceAsync<PackageMetadataResource>();
            var parametersNugetPackages = await resource.GetMetadataAsync(
                "Material.Avalonia",
                includePrerelease: true,
                includeUnlisted: false,
                cache,
                logger,
                cancellationToken);

            var packages = parametersNugetPackages as IPackageSearchMetadata[] ?? parametersNugetPackages.ToArray();
            Parameters.NugetPackages = packages;

            Log.Information("Fetched {PackagesCount} package versions from nuget", packages.Length);
        });

    Target AppendNightlyVersionIfNeeded => _ => _
        .DependsOn(FetchNuget)
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

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() => {
            DotNetTasks.DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(Parameters.Version)
                .EnableNoRestore());
        });

    Target CreateIntermediateNugetPackages => _ => _
        .DependsOn(Compile)
        .DependsOn(AppendNightlyVersionIfNeeded)
        .Executes(() => {
            DotNetTasks.DotNetPack(s => s
                .SetProject(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(Parameters.Version)
                .SetOutputDirectory(NugetIntermediateRoot));
        });

    Target CreateNugetPackages => _ => _
        .DependsOn(CreateIntermediateNugetPackages)
        .Produces(NugetRoot / "*.nupkg")
        .Executes(() => {
            var config = Numerge.MergeConfiguration.LoadFile(RootDirectory / "build" / "numerge.config.json");
            NugetRoot.CreateOrCleanDirectory();
            if (!Numerge.NugetPackageMerger.Merge(NugetIntermediateRoot, NugetRoot, config,
                    new NumergeNukeLogger()))
                throw new Exception("Package merge failed");
        });
    
    Target PublishNugetPackages => _ => _
        .OnlyWhenDynamic(() => Parameters.NugetApiKey is not null)
        .DependsOn(CreateNugetPackages)
        .Executes(() => {
            DotNetTasks.DotNetNuGetPush(s => s
                .SetSource(Parameters.NugetFeedUrl)
                .SetApiKey(Parameters.NugetApiKey)
                .SetTargetPath(NugetRoot)
                .EnableSkipDuplicate());
        });

    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    /// <inheritdoc />
    protected override void OnBuildInitialized() {
        Parameters = new BuildParameters(this);
    }
}