using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended
{
    public class BrownSwatch : ISwatch
    {
        public static Color Brown50 { get; } = Color.Parse("#EFEBE9");
        public static Color Brown100 { get; } = Color.Parse("#D7CCC8");
        public static Color Brown200 { get; } = Color.Parse("#BCAAA4");
        public static Color Brown300 { get; } = Color.Parse("#A1887F");
        public static Color Brown400 { get; } = Color.Parse("#8D6E63");
        public static Color Brown500 { get; } = Color.Parse("#795548");
        public static Color Brown600 { get; } = Color.Parse("#6D4C41");
        public static Color Brown700 { get; } = Color.Parse("#5D4037");
        public static Color Brown800 { get; } = Color.Parse("#4E342E");
        public static Color Brown900 { get; } = Color.Parse("#3E2723");

        public string Name { get; } = "Brown";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color>
        {
            {MaterialColor.Brown50, Brown50},
            {MaterialColor.Brown100, Brown100},
            {MaterialColor.Brown200, Brown200},
            {MaterialColor.Brown300, Brown300},
            {MaterialColor.Brown400, Brown400},
            {MaterialColor.Brown500, Brown500},
            {MaterialColor.Brown600, Brown600},
            {MaterialColor.Brown700, Brown700},
            {MaterialColor.Brown800, Brown800},
            {MaterialColor.Brown900, Brown900}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}