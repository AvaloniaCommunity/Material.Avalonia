using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class OrangeSwatch : ISwatch {
        public static Color Orange50 { get; } = Color.Parse("#FFF3E0");
        public static Color Orange100 { get; } = Color.Parse("#FFE0B2");
        public static Color Orange200 { get; } = Color.Parse("#FFCC80");
        public static Color Orange300 { get; } = Color.Parse("#FFB74D");
        public static Color Orange400 { get; } = Color.Parse("#FFA726");
        public static Color Orange500 { get; } = Color.Parse("#FF9800");
        public static Color Orange600 { get; } = Color.Parse("#FB8C00");
        public static Color Orange700 { get; } = Color.Parse("#F57C00");
        public static Color Orange800 { get; } = Color.Parse("#EF6C00");
        public static Color Orange900 { get; } = Color.Parse("#E65100");
        public static Color OrangeA100 { get; } = Color.Parse("#FFD180");
        public static Color OrangeA200 { get; } = Color.Parse("#FFAB40");
        public static Color OrangeA400 { get; } = Color.Parse("#FF9100");
        public static Color OrangeA700 { get; } = Color.Parse("#FF6D00");

        public string Name { get; } = "Orange";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Orange50, Orange50},
            {MaterialColor.Orange100, Orange100},
            {MaterialColor.Orange200, Orange200},
            {MaterialColor.Orange300, Orange300},
            {MaterialColor.Orange400, Orange400},
            {MaterialColor.Orange500, Orange500},
            {MaterialColor.Orange600, Orange600},
            {MaterialColor.Orange700, Orange700},
            {MaterialColor.Orange800, Orange800},
            {MaterialColor.Orange900, Orange900},
            {MaterialColor.OrangeA100, OrangeA100},
            {MaterialColor.OrangeA200, OrangeA200},
            {MaterialColor.OrangeA400, OrangeA400},
            {MaterialColor.OrangeA700, OrangeA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}