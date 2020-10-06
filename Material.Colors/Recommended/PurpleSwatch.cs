using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class PurpleSwatch : ISwatch {
        public static Color Purple50 { get; } = Color.Parse("#F3E5F5");
        public static Color Purple100 { get; } = Color.Parse("#E1BEE7");
        public static Color Purple200 { get; } = Color.Parse("#CE93D8");
        public static Color Purple300 { get; } = Color.Parse("#BA68C8");
        public static Color Purple400 { get; } = Color.Parse("#AB47BC");
        public static Color Purple500 { get; } = Color.Parse("#9C27B0");
        public static Color Purple600 { get; } = Color.Parse("#8E24AA");
        public static Color Purple700 { get; } = Color.Parse("#7B1FA2");
        public static Color Purple800 { get; } = Color.Parse("#6A1B9A");
        public static Color Purple900 { get; } = Color.Parse("#4A148C");
        public static Color PurpleA100 { get; } = Color.Parse("#EA80FC");
        public static Color PurpleA200 { get; } = Color.Parse("#E040FB");
        public static Color PurpleA400 { get; } = Color.Parse("#D500F9");
        public static Color PurpleA700 { get; } = Color.Parse("#AA00FF");

        public string Name { get; } = "Purple";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Purple50, Purple50},
            {MaterialColor.Purple100, Purple100},
            {MaterialColor.Purple200, Purple200},
            {MaterialColor.Purple300, Purple300},
            {MaterialColor.Purple400, Purple400},
            {MaterialColor.Purple500, Purple500},
            {MaterialColor.Purple600, Purple600},
            {MaterialColor.Purple700, Purple700},
            {MaterialColor.Purple800, Purple800},
            {MaterialColor.Purple900, Purple900},
            {MaterialColor.PurpleA100, PurpleA100},
            {MaterialColor.PurpleA200, PurpleA200},
            {MaterialColor.PurpleA400, PurpleA400},
            {MaterialColor.PurpleA700, PurpleA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}