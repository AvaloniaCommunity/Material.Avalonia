using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors.Recommended
{
    public class BlueGreySwatch : ISwatch
    {
        public static Color BlueGrey50 { get; } = Color.Parse("#ECEFF1");
        public static Color BlueGrey100 { get; } = Color.Parse("#CFD8DC");
        public static Color BlueGrey200 { get; } = Color.Parse("#B0BEC5");
        public static Color BlueGrey300 { get; } = Color.Parse("#90A4AE");
        public static Color BlueGrey400 { get; } = Color.Parse("#78909C");
        public static Color BlueGrey500 { get; } = Color.Parse("#607D8B");
        public static Color BlueGrey600 { get; } = Color.Parse("#546E7A");
        public static Color BlueGrey700 { get; } = Color.Parse("#455A64");
        public static Color BlueGrey800 { get; } = Color.Parse("#37474F");
        public static Color BlueGrey900 { get; } = Color.Parse("#263238");

        public string Name { get; } = "Blue Grey";

        public IDictionary<MaterialColor, Color> Lookup { get; } = new Dictionary<MaterialColor, Color>
        {
            {MaterialColor.BlueGrey50, BlueGrey50},
            {MaterialColor.BlueGrey100, BlueGrey100},
            {MaterialColor.BlueGrey200, BlueGrey200},
            {MaterialColor.BlueGrey300, BlueGrey300},
            {MaterialColor.BlueGrey400, BlueGrey400},
            {MaterialColor.BlueGrey500, BlueGrey500},
            {MaterialColor.BlueGrey600, BlueGrey600},
            {MaterialColor.BlueGrey700, BlueGrey700},
            {MaterialColor.BlueGrey800, BlueGrey800},
            {MaterialColor.BlueGrey900, BlueGrey900}
        };

        public IEnumerable<Color> Hues => Lookup.Values;
    }
}