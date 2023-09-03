using Avalonia.Media;
using Material.Colors.ColorManipulation;

namespace Material.Colors {
    public struct ColorPair {
        public Color Color { get; }

        /// <summary>
        ///     The foreground or opposite color. If left null, this will be calculated for you.
        ///     Calculated by calling ColorHelper.ContrastingForegroundColor()
        /// </summary>
        public Color ForegroundColor { get; }

        public static implicit operator ColorPair(Color color) {
            return new ColorPair(color);
        }

        public ColorPair(Color color) {
            Color = color;
            ForegroundColor = color.PickContrastColor();
        }

        public ColorPair(Color color, Color foreground) {
            Color = color;
            ForegroundColor = foreground;
        }
    }
}