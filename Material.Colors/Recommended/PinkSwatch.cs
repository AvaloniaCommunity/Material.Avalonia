using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class PinkSwatch : ISwatch {
        public static Color Pink50 { get; } = Color.Parse("#FCE4EC");
        public static Color Pink100 { get; } = Color.Parse("#F8BBD0");
        public static Color Pink200 { get; } = Color.Parse("#F48FB1");
        public static Color Pink300 { get; } = Color.Parse("#F06292");
        public static Color Pink400 { get; } = Color.Parse("#EC407A");
        public static Color Pink500 { get; } = Color.Parse("#E91E63");
        public static Color Pink600 { get; } = Color.Parse("#D81B60");
        public static Color Pink700 { get; } = Color.Parse("#C2185B");
        public static Color Pink800 { get; } = Color.Parse("#AD1457");
        public static Color Pink900 { get; } = Color.Parse("#880E4F");
        public static Color PinkA100 { get; } = Color.Parse("#FF80AB");
        public static Color PinkA200 { get; } = Color.Parse("#FF4081");
        public static Color PinkA400 { get; } = Color.Parse("#F50057");
        public static Color PinkA700 { get; } = Color.Parse("#C51162");

        public string Name { get; } = "Pink";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Pink50, Pink50},
            {MaterialColor.Pink100, Pink100},
            {MaterialColor.Pink200, Pink200},
            {MaterialColor.Pink300, Pink300},
            {MaterialColor.Pink400, Pink400},
            {MaterialColor.Pink500, Pink500},
            {MaterialColor.Pink600, Pink600},
            {MaterialColor.Pink700, Pink700},
            {MaterialColor.Pink800, Pink800},
            {MaterialColor.Pink900, Pink900},
            {MaterialColor.PinkA100, PinkA100},
            {MaterialColor.PinkA200, PinkA200},
            {MaterialColor.PinkA400, PinkA400},
            {MaterialColor.PinkA700, PinkA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}