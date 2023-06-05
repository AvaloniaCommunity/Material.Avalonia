using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended
{
    public class DeepPurpleSwatch : ISwatch
    {
        public static Color DeepPurple50 { get; } = Color.Parse("#EDE7F6");
        public static Color DeepPurple100 { get; } = Color.Parse("#D1C4E9");
        public static Color DeepPurple200 { get; } = Color.Parse("#B39DDB");
        public static Color DeepPurple300 { get; } = Color.Parse("#9575CD");
        public static Color DeepPurple400 { get; } = Color.Parse("#7E57C2");
        public static Color DeepPurple500 { get; } = Color.Parse("#673AB7");
        public static Color DeepPurple600 { get; } = Color.Parse("#5E35B1");
        public static Color DeepPurple700 { get; } = Color.Parse("#512DA8");
        public static Color DeepPurple800 { get; } = Color.Parse("#4527A0");
        public static Color DeepPurple900 { get; } = Color.Parse("#311B92");
        public static Color DeepPurpleA100 { get; } = Color.Parse("#B388FF");
        public static Color DeepPurpleA200 { get; } = Color.Parse("#7C4DFF");
        public static Color DeepPurpleA400 { get; } = Color.Parse("#651FFF");
        public static Color DeepPurpleA700 { get; } = Color.Parse("#6200EA");

        public string Name { get; } = "Deep Purple";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color>
        {
            {MaterialColor.DeepPurple50, DeepPurple50},
            {MaterialColor.DeepPurple100, DeepPurple100},
            {MaterialColor.DeepPurple200, DeepPurple200},
            {MaterialColor.DeepPurple300, DeepPurple300},
            {MaterialColor.DeepPurple400, DeepPurple400},
            {MaterialColor.DeepPurple500, DeepPurple500},
            {MaterialColor.DeepPurple600, DeepPurple600},
            {MaterialColor.DeepPurple700, DeepPurple700},
            {MaterialColor.DeepPurple800, DeepPurple800},
            {MaterialColor.DeepPurple900, DeepPurple900},
            {MaterialColor.DeepPurpleA100, DeepPurpleA100},
            {MaterialColor.DeepPurpleA200, DeepPurpleA200},
            {MaterialColor.DeepPurpleA400, DeepPurpleA400},
            {MaterialColor.DeepPurpleA700, DeepPurpleA700}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}