using System.ComponentModel;

namespace Material.Avalonia.Demo.Models;

public enum StatusEnum {
    Yes,
    No,
    [Description("N/A")] NA,
    [Description("Not Fully")] NotFully
}

public class FeatureStatusModels {
    public required string FeatureName { get; init; }
    public StatusEnum IsReady { get; init; }
    public StatusEnum IsAnimated { get; init; }
}