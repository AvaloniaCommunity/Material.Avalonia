using System.Collections.Generic;
using Avalonia.Media;

namespace Material.Colors {
    public interface ISwatch {
        string Name { get; }
        IEnumerable<Color> Hues { get; }
        IDictionary<MaterialColor, Color> Lookup { get; }
    }
}