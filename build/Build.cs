using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Nuke.Common.Tools.MinVer;
using Nuke.Common.Utilities;
using Numerge;
using Serilog;
// ReSharper disable AllUnderscoreLocalParameterName
// ReSharper disable InconsistentNaming

partial class Build : NukeBuild {
    [GitRepository] readonly GitRepository Repository = null!;
    [Solution] readonly Solution Solution = null!;
    [MinVer] MinVer MinVer = null!;
    BuildParameters Parameters { get; set; } = null!;

    AbsolutePath ArtifactsDir => RootDirectory / "artifacts";
    AbsolutePath DemoDir => ArtifactsDir / "demo";
    AbsolutePath NugetIntermediateRoot => ArtifactsDir / "build-intermediate" / "nuget";
    AbsolutePath NugetRoot => ArtifactsDir / "nuget";

    Target Clean => _ => _
        .Executes(() => {
            DemoDir.CreateOrCleanDirectory();
            NugetIntermediateRoot.DeleteDirectory();
            NugetRoot.CreateOrCleanDirectory();
        });

    Target Compile => _ => _
        .DependsOn(Clean)
        .Executes(() => {
            DotNetTasks.DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(Parameters.Version.ToString()));
        });

    Target CreateIntermediateNugetPackages => _ => _
        .DependsOn(Compile)
        .Executes(() => {
            DotNetTasks.DotNetPack(s => {
                s = s
                    .SetProject(Solution)
                    .SetConfiguration(Configuration)
                    .SetVersion(Parameters.Version.ToString())
                    .SetOutputDirectory(NugetIntermediateRoot)
                    .EnableDeterministic()
                    .EnableDeterministicSourcePaths();

                if (Parameters.Version.IsNightly()) {
                    var commitMessage = GitTasks.Git("show -s --format=%s")
                        .Select(o => o.Text)
                        .Join("\n");

                    s = s.SetPackageReleaseNotes($"Nightly version for commit {Repository.Commit}.\n{commitMessage}");
                }
                
                return s;
            });
        });

    Target PackNugetPackages => _ => _
        .DependsOn(CreateIntermediateNugetPackages)
        .Produces(NugetRoot / "*.nupkg")
        .Executes(() => {
            var config = MergeConfiguration.LoadFile(RootDirectory / "build" / "numerge.config.json");
            NugetRoot.CreateOrCleanDirectory();
            if (!NugetPackageMerger.Merge(NugetIntermediateRoot, NugetRoot, config,
                    new NumergeNukeLogger()))
                throw new Exception("Package merge failed");
        });

    Target PackDemoApp => _ => _
        .DependsOn(Clean)
        .Produces(DemoDir)
        .Executes(() => {
            PublishFor("win-x64", ".exe");
            PublishFor("linux-x64");

            void PublishFor(string rid, string fileExtension = "") {
                DotNetTasks.DotNetPublish(s => s
                    .SetProject(Solution.GetProject("Material.Demo"))
                    .SetConfiguration("Release")
                    .SetVersion(Parameters.Version.ToString())
                    .SetOutput(DemoDir)
                    .SetProperty("IncludeNativeLibrariesForSelfExtract", "true")
                    .SetProperty("IncludeAllContentForSelfExtract", "true")
                    .SetProperty("DebugSymbols", false)
                    .SetProperty("DebugType", "none")
                    .EnableSelfContained()
                    .EnablePublishSingleFile()
                    .SetProperty("PublishAot", false)
                    .SetRuntime(rid));

                var binaryFile = (DemoDir / "Material.Demo" + fileExtension).ToFileInfo();
                binaryFile.MoveTo(DemoDir / $"Material.Demo_{rid}{fileExtension}");
            }
        });

    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    /// <inheritdoc />
    protected override void OnBuildInitialized() {
        MinVer = MinVerTasks.MinVer(s => s
                .DisableProcessLogOutput()
                .SetTagPrefix("v")
                .SetDefaultPreReleasePhase("nightly"))
            .Result;

        Parameters = new BuildParameters(this);

        Log.Information("Building version {Version} of Material.Avalonia ({Configuration}) using version {NukeVersion} of Nuke",
            Parameters.Version, Parameters.Configuration, typeof(NukeBuild).Assembly.GetName().Version!.ToString());
    }
}