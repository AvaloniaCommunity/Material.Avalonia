using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class CyanSwatch : ISwatch {
        public static Color Cyan50 { get; } = Color.Parse("#E0F7FA");
        public static Color Cyan100 { get; } = Color.Parse("#B2EBF2");
        public static Color Cyan200 { get; } = Color.Parse("#80DEEA");
        public static Color Cyan300 { get; } = Color.Parse("#4DD0E1");
        public static Color Cyan400 { get; } = Color.Parse("#26C6DA");
        public static Color Cyan500 { get; } = Color.Parse("#00BCD4");
        public static Color Cyan600 { get; } = Color.Parse("#00ACC1");
        public static Color Cyan700 { get; } = Color.Parse("#0097A7");
        public static Color Cyan800 { get; } = Color.Parse("#00838F");
        public static Color Cyan900 { get; } = Color.Parse("#006064");
        public static Color CyanA100 { get; } = Color.Parse("#84FFFF");
        public static Color CyanA200 { get; } = Color.Parse("#18FFFF");
        public static Color CyanA400 { get; } = Color.Parse("#00E5FF");
        public static Color CyanA700 { get; } = Color.Parse("#00B8D4");

        public string Name { get; } = "Cyan";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Cyan50, Cyan50},
            {MaterialColor.Cyan100, Cyan100},
            {MaterialColor.Cyan200, Cyan200},
            {MaterialColor.Cyan300, Cyan300},
            {MaterialColor.Cyan400, Cyan400},
            {MaterialColor.Cyan500, Cyan500},
            {MaterialColor.Cyan600, Cyan600},
            {MaterialColor.Cyan700, Cyan700},
            {MaterialColor.Cyan800, Cyan800},
            {MaterialColor.Cyan900, Cyan900},
            {MaterialColor.CyanA100, CyanA100},
            {MaterialColor.CyanA200, CyanA200},
            {MaterialColor.CyanA400, CyanA400},
            {MaterialColor.CyanA700, CyanA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}