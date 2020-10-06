using System.Collections.Generic;
using Avalonia.Media;

namespace MaterialColors.Recommended {
    public class IndigoSwatch : ISwatch {
        public static Color Indigo50 { get; } = Color.Parse("#E8EAF6");
        public static Color Indigo100 { get; } = Color.Parse("#C5CAE9");
        public static Color Indigo200 { get; } = Color.Parse("#9FA8DA");
        public static Color Indigo300 { get; } = Color.Parse("#7986CB");
        public static Color Indigo400 { get; } = Color.Parse("#5C6BC0");
        public static Color Indigo500 { get; } = Color.Parse("#3F51B5");
        public static Color Indigo600 { get; } = Color.Parse("#3949AB");
        public static Color Indigo700 { get; } = Color.Parse("#303F9F");
        public static Color Indigo800 { get; } = Color.Parse("#283593");
        public static Color Indigo900 { get; } = Color.Parse("#1A237E");
        public static Color IndigoA100 { get; } = Color.Parse("#8C9EFF");
        public static Color IndigoA200 { get; } = Color.Parse("#536DFE");
        public static Color IndigoA400 { get; } = Color.Parse("#3D5AFE");
        public static Color IndigoA700 { get; } = Color.Parse("#304FFE");

        public string Name { get; } = "Indigo";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color> {
            {MaterialColor.Indigo50, Indigo50},
            {MaterialColor.Indigo100, Indigo100},
            {MaterialColor.Indigo200, Indigo200},
            {MaterialColor.Indigo300, Indigo300},
            {MaterialColor.Indigo400, Indigo400},
            {MaterialColor.Indigo500, Indigo500},
            {MaterialColor.Indigo600, Indigo600},
            {MaterialColor.Indigo700, Indigo700},
            {MaterialColor.Indigo800, Indigo800},
            {MaterialColor.Indigo900, Indigo900},
            {MaterialColor.IndigoA100, IndigoA100},
            {MaterialColor.IndigoA200, IndigoA200},
            {MaterialColor.IndigoA400, IndigoA400},
            {MaterialColor.IndigoA700, IndigoA700},
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}