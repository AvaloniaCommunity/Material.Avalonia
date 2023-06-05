using System;
using Avalonia.Media;

namespace Material.Colors.ColorManipulation {
    internal static class LabConverter {
        public static Lab ToLab(this Color c) {
            var xyz = c.ToXyz();
            return xyz.ToLab();
        }

        public static Lab ToLab(this Xyz xyz) {
            double XyzLab(double v) {
                if (v > LabConstants.E)
                    return Math.Pow(v, 1 / 3.0);
                return (v * LabConstants.K + 16) / 116;
            }

            var fx = XyzLab(xyz.X / LabConstants.WhitePointX);
            var fy = XyzLab(xyz.Y / LabConstants.WhitePointY);
            var fz = XyzLab(xyz.Z / LabConstants.WhitePointZ);

            var l = 116 * fy - 16;
            var a = 500 * (fx - fy);
            var b = 200 * (fy - fz);
            return new Lab(l, a, b);
        }

        public static Color ToColor(this Lab lab) {
            var xyz = lab.ToXyz();

            return xyz.ToColor();
        }
    }
}
