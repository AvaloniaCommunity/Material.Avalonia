using System.Collections.Generic;
using Avalonia.Media;

namespace MaterialColors.Recommended {
    public class TealSwatch : ISwatch {
        public static Color Teal50 { get; } = Color.Parse("#E0F2F1");
        public static Color Teal100 { get; } = Color.Parse("#B2DFDB");
        public static Color Teal200 { get; } = Color.Parse("#80CBC4");
        public static Color Teal300 { get; } = Color.Parse("#4DB6AC");
        public static Color Teal400 { get; } = Color.Parse("#26A69A");
        public static Color Teal500 { get; } = Color.Parse("#009688");
        public static Color Teal600 { get; } = Color.Parse("#00897B");
        public static Color Teal700 { get; } = Color.Parse("#00796B");
        public static Color Teal800 { get; } = Color.Parse("#00695C");
        public static Color Teal900 { get; } = Color.Parse("#004D40");
        public static Color TealA100 { get; } = Color.Parse("#A7FFEB");
        public static Color TealA200 { get; } = Color.Parse("#64FFDA");
        public static Color TealA400 { get; } = Color.Parse("#1DE9B6");
        public static Color TealA700 { get; } = Color.Parse("#00BFA5");

        public string Name { get; } = "Teal";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Teal50, Teal50},
            {MaterialColor.Teal100, Teal100},
            {MaterialColor.Teal200, Teal200},
            {MaterialColor.Teal300, Teal300},
            {MaterialColor.Teal400, Teal400},
            {MaterialColor.Teal500, Teal500},
            {MaterialColor.Teal600, Teal600},
            {MaterialColor.Teal700, Teal700},
            {MaterialColor.Teal800, Teal800},
            {MaterialColor.Teal900, Teal900},
            {MaterialColor.TealA100, TealA100},
            {MaterialColor.TealA200, TealA200},
            {MaterialColor.TealA400, TealA400},
            {MaterialColor.TealA700, TealA700},
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}