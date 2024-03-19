using System;
using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
public partial class Build {
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter(Name = "force-version")]
    public string? ForceVersion { get; set; }

    [Parameter(Name = "nuget-feed-url")]
    public string NuGetFeedUrl { get; set; } = "https://api.nuget.org/v3/index.json";

    [Secret]
    [Parameter(Name = "nuget-api-key")]
    public string? NuGetApiKey { get; set; }

    public sealed class BuildParameters {
        public BuildParameters(Build b) {
            // ARGUMENTS
            Configuration = b.Configuration;

            // CONFIGURATION
            MainRepo = "https://github.com/AvaloniaCommunity/Material.Avalonia";
            MasterBranch = "refs/heads/master";

            // PARAMETERS
            NugetFeedUrl = b.NuGetFeedUrl;
            NugetApiKey = b.NuGetApiKey;

            IsRunningOnGitHubActions = Host is GitHubActions;
            if (IsRunningOnGitHubActions) {
                RepositoryName = $"{GitHubActions.Instance.ServerUrl}/{GitHubActions.Instance.Repository}";
                RepositoryBranch = GitHubActions.Instance.Ref;
                IsReleasableBranch = StringComparer.OrdinalIgnoreCase.Equals(MasterBranch, RepositoryBranch)
                                  || GitHubActions.Instance.Ref.StartsWith("refs/heads/release/")
                                  || StringComparer.OrdinalIgnoreCase.Equals(GitHubActions.Instance.RefType, "tag");
            }

            IsMainRepo = StringComparer.OrdinalIgnoreCase.Equals(MainRepo, RepositoryName);
            IsReleasable = Configuration.Release == Configuration;

            ShouldPublishNugetPackages = IsRunningOnGitHubActions && IsReleasableBranch && IsReleasable
                                      && (b.MinVer.MinVerPreRelease is null || !b.MinVer.MinVerPreRelease.EndsWith(".0"));

            // VERSION
            Version = new NuGetVersion(b.ForceVersion ?? b.MinVer.Version);
        }

        public Configuration Configuration { get; }
        public string MainRepo { get; }
        public string MasterBranch { get; }
        public string RepositoryBranch { get; } = null!;
        public string RepositoryName { get; } = null!;
        public bool IsRunningOnGitHubActions { get; }
        public bool IsMainRepo { get; }
        public bool IsReleasableBranch { get; }
        public bool IsReleasable { get; }
        public NuGetVersion Version { get; set; }
        public bool ShouldPublishNugetPackages { get; set; }
        public string NugetFeedUrl { get; set; }
        public string? NugetApiKey { get; set; }
    }
}