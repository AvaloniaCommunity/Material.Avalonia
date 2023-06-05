using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class DeepOrangeSwatch : ISwatch {
        public static Color DeepOrange50 { get; } = Color.Parse("#FBE9E7");
        public static Color DeepOrange100 { get; } = Color.Parse("#FFCCBC");
        public static Color DeepOrange200 { get; } = Color.Parse("#FFAB91");
        public static Color DeepOrange300 { get; } = Color.Parse("#FF8A65");
        public static Color DeepOrange400 { get; } = Color.Parse("#FF7043");
        public static Color DeepOrange500 { get; } = Color.Parse("#FF5722");
        public static Color DeepOrange600 { get; } = Color.Parse("#F4511E");
        public static Color DeepOrange700 { get; } = Color.Parse("#E64A19");
        public static Color DeepOrange800 { get; } = Color.Parse("#D84315");
        public static Color DeepOrange900 { get; } = Color.Parse("#BF360C");
        public static Color DeepOrangeA100 { get; } = Color.Parse("#FF9E80");
        public static Color DeepOrangeA200 { get; } = Color.Parse("#FF6E40");
        public static Color DeepOrangeA400 { get; } = Color.Parse("#FF3D00");
        public static Color DeepOrangeA700 { get; } = Color.Parse("#DD2C00");

        public string Name { get; } = "Deep Orange";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            { MaterialColor.DeepOrange50, DeepOrange50 },
            { MaterialColor.DeepOrange100, DeepOrange100 },
            { MaterialColor.DeepOrange200, DeepOrange200 },
            { MaterialColor.DeepOrange300, DeepOrange300 },
            { MaterialColor.DeepOrange400, DeepOrange400 },
            { MaterialColor.DeepOrange500, DeepOrange500 },
            { MaterialColor.DeepOrange600, DeepOrange600 },
            { MaterialColor.DeepOrange700, DeepOrange700 },
            { MaterialColor.DeepOrange800, DeepOrange800 },
            { MaterialColor.DeepOrange900, DeepOrange900 },
            { MaterialColor.DeepOrangeA100, DeepOrangeA100 },
            { MaterialColor.DeepOrangeA200, DeepOrangeA200 },
            { MaterialColor.DeepOrangeA400, DeepOrangeA400 },
            { MaterialColor.DeepOrangeA700, DeepOrangeA700 }
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
