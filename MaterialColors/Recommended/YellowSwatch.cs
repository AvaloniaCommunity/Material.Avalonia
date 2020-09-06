using System.Collections.Generic;
using Avalonia.Media;

namespace MaterialColors.Recommended {
    public class YellowSwatch : ISwatch {
        public static Color Yellow50 { get; } = Color.Parse("#FFFDE7");
        public static Color Yellow100 { get; } = Color.Parse("#FFF9C4");
        public static Color Yellow200 { get; } = Color.Parse("#FFF59D");
        public static Color Yellow300 { get; } = Color.Parse("#FFF176");
        public static Color Yellow400 { get; } = Color.Parse("#FFEE58");
        public static Color Yellow500 { get; } = Color.Parse("#FFEB3B");
        public static Color Yellow600 { get; } = Color.Parse("#FDD835");
        public static Color Yellow700 { get; } = Color.Parse("#FBC02D");
        public static Color Yellow800 { get; } = Color.Parse("#F9A825");
        public static Color Yellow900 { get; } = Color.Parse("#F57F17");
        public static Color YellowA100 { get; } = Color.Parse("#FFFF8D");
        public static Color YellowA200 { get; } = Color.Parse("#FFFF00");
        public static Color YellowA400 { get; } = Color.Parse("#FFEA00");
        public static Color YellowA700 { get; } = Color.Parse("#FFD600");

        public string Name { get; } = "Yellow";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Yellow50, Yellow50},
            {MaterialColor.Yellow100, Yellow100},
            {MaterialColor.Yellow200, Yellow200},
            {MaterialColor.Yellow300, Yellow300},
            {MaterialColor.Yellow400, Yellow400},
            {MaterialColor.Yellow500, Yellow500},
            {MaterialColor.Yellow600, Yellow600},
            {MaterialColor.Yellow700, Yellow700},
            {MaterialColor.Yellow800, Yellow800},
            {MaterialColor.Yellow900, Yellow900},
            {MaterialColor.YellowA100, YellowA100},
            {MaterialColor.YellowA200, YellowA200},
            {MaterialColor.YellowA400, YellowA400},
            {MaterialColor.YellowA700, YellowA700},
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}