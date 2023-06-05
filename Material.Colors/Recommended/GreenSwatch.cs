using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class GreenSwatch : ISwatch {
        public static Color Green50 { get; } = Color.Parse("#E8F5E9");
        public static Color Green100 { get; } = Color.Parse("#C8E6C9");
        public static Color Green200 { get; } = Color.Parse("#A5D6A7");
        public static Color Green300 { get; } = Color.Parse("#81C784");
        public static Color Green400 { get; } = Color.Parse("#66BB6A");
        public static Color Green500 { get; } = Color.Parse("#4CAF50");
        public static Color Green600 { get; } = Color.Parse("#43A047");
        public static Color Green700 { get; } = Color.Parse("#388E3C");
        public static Color Green800 { get; } = Color.Parse("#2E7D32");
        public static Color Green900 { get; } = Color.Parse("#1B5E20");
        public static Color GreenA100 { get; } = Color.Parse("#B9F6CA");
        public static Color GreenA200 { get; } = Color.Parse("#69F0AE");
        public static Color GreenA400 { get; } = Color.Parse("#00E676");
        public static Color GreenA700 { get; } = Color.Parse("#00C853");

        public string Name { get; } = "Green";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            { MaterialColor.Green50, Green50 },
            { MaterialColor.Green100, Green100 },
            { MaterialColor.Green200, Green200 },
            { MaterialColor.Green300, Green300 },
            { MaterialColor.Green400, Green400 },
            { MaterialColor.Green500, Green500 },
            { MaterialColor.Green600, Green600 },
            { MaterialColor.Green700, Green700 },
            { MaterialColor.Green800, Green800 },
            { MaterialColor.Green900, Green900 },
            { MaterialColor.GreenA100, GreenA100 },
            { MaterialColor.GreenA200, GreenA200 },
            { MaterialColor.GreenA400, GreenA400 },
            { MaterialColor.GreenA700, GreenA700 }
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
