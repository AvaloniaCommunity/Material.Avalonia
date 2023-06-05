using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended {
    public class GreySwatch : ISwatch {
        public static Color Grey50 { get; } = Color.Parse("#FAFAFA");
        public static Color Grey100 { get; } = Color.Parse("#F5F5F5");
        public static Color Grey200 { get; } = Color.Parse("#EEEEEE");
        public static Color Grey300 { get; } = Color.Parse("#E0E0E0");
        public static Color Grey400 { get; } = Color.Parse("#BDBDBD");
        public static Color Grey500 { get; } = Color.Parse("#9E9E9E");
        public static Color Grey600 { get; } = Color.Parse("#757575");
        public static Color Grey700 { get; } = Color.Parse("#616161");
        public static Color Grey800 { get; } = Color.Parse("#424242");
        public static Color Grey900 { get; } = Color.Parse("#212121");

        public string Name { get; } = "Grey";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            { MaterialColor.Grey50, Grey50 },
            { MaterialColor.Grey100, Grey100 },
            { MaterialColor.Grey200, Grey200 },
            { MaterialColor.Grey300, Grey300 },
            { MaterialColor.Grey400, Grey400 },
            { MaterialColor.Grey500, Grey500 },
            { MaterialColor.Grey600, Grey600 },
            { MaterialColor.Grey700, Grey700 },
            { MaterialColor.Grey800, Grey800 },
            { MaterialColor.Grey900, Grey900 }
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}
