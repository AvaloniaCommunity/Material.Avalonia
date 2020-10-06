using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class BlueSwatch : ISwatch {
        public static Color Blue50 { get; } = Color.Parse("#E3F2FD");
        public static Color Blue100 { get; } = Color.Parse("#BBDEFB");
        public static Color Blue200 { get; } = Color.Parse("#90CAF9");
        public static Color Blue300 { get; } = Color.Parse("#64B5F6");
        public static Color Blue400 { get; } = Color.Parse("#42A5F5");
        public static Color Blue500 { get; } = Color.Parse("#2196F3");
        public static Color Blue600 { get; } = Color.Parse("#1E88E5");
        public static Color Blue700 { get; } = Color.Parse("#1976D2");
        public static Color Blue800 { get; } = Color.Parse("#1565C0");
        public static Color Blue900 { get; } = Color.Parse("#0D47A1");
        public static Color BlueA100 { get; } = Color.Parse("#82B1FF");
        public static Color BlueA200 { get; } = Color.Parse("#448AFF");
        public static Color BlueA400 { get; } = Color.Parse("#2979FF");
        public static Color BlueA700 { get; } = Color.Parse("#2962FF");

        public string Name { get; } = "Blue";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Blue50, Blue50},
            {MaterialColor.Blue100, Blue100},
            {MaterialColor.Blue200, Blue200},
            {MaterialColor.Blue300, Blue300},
            {MaterialColor.Blue400, Blue400},
            {MaterialColor.Blue500, Blue500},
            {MaterialColor.Blue600, Blue600},
            {MaterialColor.Blue700, Blue700},
            {MaterialColor.Blue800, Blue800},
            {MaterialColor.Blue900, Blue900},
            {MaterialColor.BlueA100, BlueA100},
            {MaterialColor.BlueA200, BlueA200},
            {MaterialColor.BlueA400, BlueA400},
            {MaterialColor.BlueA700, BlueA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}