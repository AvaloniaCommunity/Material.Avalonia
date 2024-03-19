using System.Collections.Immutable;
using System.Linq;
using NuGet.Versioning;
public static class BuildExtensions {
    public static bool IsNightly(this NuGetVersion version) {
        if (version.ToString().Contains("nightly")) {
            return true;
        }

        // 3.2.5-nightly.0.1
        // If x.y on the end - this is nightly
        var lastLabels = version.ReleaseLabels.TakeLast(2).ToImmutableArray();
        return lastLabels.Length == 2 && lastLabels.All(s => int.TryParse(s, out _));
    }
}