using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class LightBlueSwatch : ISwatch {
        public static Color LightBlue50 { get; } = Color.Parse("#E1F5FE");
        public static Color LightBlue100 { get; } = Color.Parse("#B3E5FC");
        public static Color LightBlue200 { get; } = Color.Parse("#81D4FA");
        public static Color LightBlue300 { get; } = Color.Parse("#4FC3F7");
        public static Color LightBlue400 { get; } = Color.Parse("#29B6F6");
        public static Color LightBlue500 { get; } = Color.Parse("#03A9F4");
        public static Color LightBlue600 { get; } = Color.Parse("#039BE5");
        public static Color LightBlue700 { get; } = Color.Parse("#0288D1");
        public static Color LightBlue800 { get; } = Color.Parse("#0277BD");
        public static Color LightBlue900 { get; } = Color.Parse("#01579B");
        public static Color LightBlueA100 { get; } = Color.Parse("#80D8FF");
        public static Color LightBlueA200 { get; } = Color.Parse("#40C4FF");
        public static Color LightBlueA400 { get; } = Color.Parse("#00B0FF");
        public static Color LightBlueA700 { get; } = Color.Parse("#0091EA");

        public string Name { get; } = "Light Blue";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.LightBlue50, LightBlue50},
            {MaterialColor.LightBlue100, LightBlue100},
            {MaterialColor.LightBlue200, LightBlue200},
            {MaterialColor.LightBlue300, LightBlue300},
            {MaterialColor.LightBlue400, LightBlue400},
            {MaterialColor.LightBlue500, LightBlue500},
            {MaterialColor.LightBlue600, LightBlue600},
            {MaterialColor.LightBlue700, LightBlue700},
            {MaterialColor.LightBlue800, LightBlue800},
            {MaterialColor.LightBlue900, LightBlue900},
            {MaterialColor.LightBlueA100, LightBlueA100},
            {MaterialColor.LightBlueA200, LightBlueA200},
            {MaterialColor.LightBlueA400, LightBlueA400},
            {MaterialColor.LightBlueA700, LightBlueA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}