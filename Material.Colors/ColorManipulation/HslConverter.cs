using System;
using Avalonia.Media;

namespace Material.Colors.ColorManipulation {
    internal static class HslConverter {
        public static Color ToColor(this Hsl hsl) {
            double HsvRbg(double v1, double v2, double vH) {
                if (vH < 0) vH += 1;
                if (vH > 1) vH -= 1;
                if (6 * vH < 1) return v1 + (v2 - v1) * 6 * vH;
                if (2 * vH < 1) return v2;
                if (3 * vH < 2) return v1 + (v2 - v1) * (2.0 / 3 - vH) * 6;
                return v1;
            }

            var h = hsl.H * (1.0 / 360);
            var s = hsl.S * (1.0 / 100);
            var l = hsl.L * (1.0 / 100);

            double r, g, b;
            if (s == 0) {
                r = l * 255;
                g = l * 255;
                b = l * 255;
            }
            else {
                double var2;
                if (l < 0.5) var2 = l * (1 + s);
                else var2 = l + s - s * l;

                var var1 = 2 * l - var2;

                r = 255 * HsvRbg(var1, var2, h + 1.0 / 3);
                g = 255 * HsvRbg(var1, var2, h);
                b = 255 * HsvRbg(var1, var2, h - 1.0 / 3);
            }

            return Color.FromRgb((byte) Math.Round(r), (byte) Math.Round(g), (byte) Math.Round(b));
        }
    }
}