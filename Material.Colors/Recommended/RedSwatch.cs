using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class RedSwatch : ISwatch {
        public static Color Red50 { get; } = Color.Parse("#FFEBEE");
        public static Color Red100 { get; } = Color.Parse("#FFCDD2");
        public static Color Red200 { get; } = Color.Parse("#EF9A9A");
        public static Color Red300 { get; } = Color.Parse("#E57373");
        public static Color Red400 { get; } = Color.Parse("#EF5350");
        public static Color Red500 { get; } = Color.Parse("#F44336");
        public static Color Red600 { get; } = Color.Parse("#E53935");
        public static Color Red700 { get; } = Color.Parse("#D32F2F");
        public static Color Red800 { get; } = Color.Parse("#C62828");
        public static Color Red900 { get; } = Color.Parse("#B71C1C");
        public static Color RedA100 { get; } = Color.Parse("#FF8A80");
        public static Color RedA200 { get; } = Color.Parse("#FF5252");
        public static Color RedA400 { get; } = Color.Parse("#FF1744");
        public static Color RedA700 { get; } = Color.Parse("#D50000");

        public string Name { get; } = "Red";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            { MaterialColor.Red50, Red50 },
            { MaterialColor.Red100, Red100 },
            { MaterialColor.Red200, Red200 },
            { MaterialColor.Red300, Red300 },
            { MaterialColor.Red400, Red400 },
            { MaterialColor.Red500, Red500 },
            { MaterialColor.Red600, Red600 },
            { MaterialColor.Red700, Red700 },
            { MaterialColor.Red800, Red800 },
            { MaterialColor.Red900, Red900 },
            { MaterialColor.RedA100, RedA100 },
            { MaterialColor.RedA200, RedA200 },
            { MaterialColor.RedA400, RedA400 },
            { MaterialColor.RedA700, RedA700 }
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
