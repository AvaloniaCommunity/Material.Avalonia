using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class LightGreenSwatch : ISwatch {
        public static Color LightGreen50 { get; } = Color.Parse("#F1F8E9");
        public static Color LightGreen100 { get; } = Color.Parse("#DCEDC8");
        public static Color LightGreen200 { get; } = Color.Parse("#C5E1A5");
        public static Color LightGreen300 { get; } = Color.Parse("#AED581");
        public static Color LightGreen400 { get; } = Color.Parse("#9CCC65");
        public static Color LightGreen500 { get; } = Color.Parse("#8BC34A");
        public static Color LightGreen600 { get; } = Color.Parse("#7CB342");
        public static Color LightGreen700 { get; } = Color.Parse("#689F38");
        public static Color LightGreen800 { get; } = Color.Parse("#558B2F");
        public static Color LightGreen900 { get; } = Color.Parse("#33691E");
        public static Color LightGreenA100 { get; } = Color.Parse("#CCFF90");
        public static Color LightGreenA200 { get; } = Color.Parse("#B2FF59");
        public static Color LightGreenA400 { get; } = Color.Parse("#76FF03");
        public static Color LightGreenA700 { get; } = Color.Parse("#64DD17");

        public string Name { get; } = "Light Green";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.LightGreen50, LightGreen50},
            {MaterialColor.LightGreen100, LightGreen100},
            {MaterialColor.LightGreen200, LightGreen200},
            {MaterialColor.LightGreen300, LightGreen300},
            {MaterialColor.LightGreen400, LightGreen400},
            {MaterialColor.LightGreen500, LightGreen500},
            {MaterialColor.LightGreen600, LightGreen600},
            {MaterialColor.LightGreen700, LightGreen700},
            {MaterialColor.LightGreen800, LightGreen800},
            {MaterialColor.LightGreen900, LightGreen900},
            {MaterialColor.LightGreenA100, LightGreenA100},
            {MaterialColor.LightGreenA200, LightGreenA200},
            {MaterialColor.LightGreenA400, LightGreenA400},
            {MaterialColor.LightGreenA700, LightGreenA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}