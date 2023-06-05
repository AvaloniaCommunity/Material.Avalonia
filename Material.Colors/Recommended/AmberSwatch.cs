using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class AmberSwatch : ISwatch {
        public static Color Amber50 { get; } = Color.Parse("#FFF8E1");
        public static Color Amber100 { get; } = Color.Parse("#FFECB3");
        public static Color Amber200 { get; } = Color.Parse("#FFE082");
        public static Color Amber300 { get; } = Color.Parse("#FFD54F");
        public static Color Amber400 { get; } = Color.Parse("#FFCA28");
        public static Color Amber500 { get; } = Color.Parse("#FFC107");
        public static Color Amber600 { get; } = Color.Parse("#FFB300");
        public static Color Amber700 { get; } = Color.Parse("#FFA000");
        public static Color Amber800 { get; } = Color.Parse("#FF8F00");
        public static Color Amber900 { get; } = Color.Parse("#FF6F00");
        public static Color AmberA100 { get; } = Color.Parse("#FFE57F");
        public static Color AmberA200 { get; } = Color.Parse("#FFD740");
        public static Color AmberA400 { get; } = Color.Parse("#FFC400");
        public static Color AmberA700 { get; } = Color.Parse("#FFAB00");

        public string Name { get; } = "Amber";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            { MaterialColor.Amber50, Amber50 },
            { MaterialColor.Amber100, Amber100 },
            { MaterialColor.Amber200, Amber200 },
            { MaterialColor.Amber300, Amber300 },
            { MaterialColor.Amber400, Amber400 },
            { MaterialColor.Amber500, Amber500 },
            { MaterialColor.Amber600, Amber600 },
            { MaterialColor.Amber700, Amber700 },
            { MaterialColor.Amber800, Amber800 },
            { MaterialColor.Amber900, Amber900 },
            { MaterialColor.AmberA100, AmberA100 },
            { MaterialColor.AmberA200, AmberA200 },
            { MaterialColor.AmberA400, AmberA400 },
            { MaterialColor.AmberA700, AmberA700 }
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
