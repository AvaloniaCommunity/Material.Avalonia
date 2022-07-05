using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended
{
    public class LimeSwatch : ISwatch
    {
        public static Color Lime50 { get; } = Color.Parse("#F9FBE7");
        public static Color Lime100 { get; } = Color.Parse("#F0F4C3");
        public static Color Lime200 { get; } = Color.Parse("#E6EE9C");
        public static Color Lime300 { get; } = Color.Parse("#DCE775");
        public static Color Lime400 { get; } = Color.Parse("#D4E157");
        public static Color Lime500 { get; } = Color.Parse("#CDDC39");
        public static Color Lime600 { get; } = Color.Parse("#C0CA33");
        public static Color Lime700 { get; } = Color.Parse("#AFB42B");
        public static Color Lime800 { get; } = Color.Parse("#9E9D24");
        public static Color Lime900 { get; } = Color.Parse("#827717");
        public static Color LimeA100 { get; } = Color.Parse("#F4FF81");
        public static Color LimeA200 { get; } = Color.Parse("#EEFF41");
        public static Color LimeA400 { get; } = Color.Parse("#C6FF00");
        public static Color LimeA700 { get; } = Color.Parse("#AEEA00");

        public string Name { get; } = "Lime";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color>
        {
            {MaterialColor.Lime50, Lime50},
            {MaterialColor.Lime100, Lime100},
            {MaterialColor.Lime200, Lime200},
            {MaterialColor.Lime300, Lime300},
            {MaterialColor.Lime400, Lime400},
            {MaterialColor.Lime500, Lime500},
            {MaterialColor.Lime600, Lime600},
            {MaterialColor.Lime700, Lime700},
            {MaterialColor.Lime800, Lime800},
            {MaterialColor.Lime900, Lime900},
            {MaterialColor.LimeA100, LimeA100},
            {MaterialColor.LimeA200, LimeA200},
            {MaterialColor.LimeA400, LimeA400},
            {MaterialColor.LimeA700, LimeA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}