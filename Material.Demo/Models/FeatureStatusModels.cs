using System.ComponentModel;

namespace Material.Demo.Models {
    public enum StatusEnum {
        Yes,
        No,
        [Description("N/A")] NA,
        [Description("Not Fully")] NotFully
    }

    public class FeatureStatusModels {
        public string FeatureName { get; internal set; }
        public StatusEnum IsReady { get; internal set; }
        public StatusEnum IsAnimated { get; internal set; }
    }
}
