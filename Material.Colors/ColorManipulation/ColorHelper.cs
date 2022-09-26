using System;
using Avalonia.Media;

namespace Material.Colors.ColorManipulation
{
    public static class ColorHelper
    {
        [Obsolete("Please use PickContrastColor method instead.")]
        public static Color ContrastingForegroundColor(this Color color) =>
            PickContrastColor(color);

        /// <summary>
        /// Choose most accessible color by algorithm. The primary and secondary color is pure black and white.
        /// </summary>
        /// <param name="color">Background color</param>
        /// <param name="ratio">Minimal contrast ratio. It is 4.5 by default.</param>
        /// <returns>The most accessible color (AAA or AA level) for text</returns>
        public static Color PickContrastColor(this Color color, double ratio = 4.5)
        {
            return AlgorithmContrastColor(color, Avalonia.Media.Colors.Black, Avalonia.Media.Colors.White, ratio);
        }

        /// <summary>
        /// Choose most accessible color by algorithm.
        /// </summary>
        /// <param name="color">Background color</param>
        /// <param name="a">Primary accessible color</param>
        /// <param name="b">Secondary accessible color</param>
        /// <param name="ratio">Minimal contrast ratio. It is 4.5 by default.</param>
        /// <returns>The most accessible color for text or control (not guarantee its accessible because the primary and secondary colors might not most used on UIs.)</returns>
        public static Color PickContrastColor(this Color color, Color a, Color b, double ratio = 4.5)
        {
            return AlgorithmContrastColor(color, a, b, ratio);
        }

        public static Color ShiftLightness(this Color color, int amount = 1)
        {
            var lab = color.ToLab();
            var shifted = new Lab(lab.L - LabConstants.Kn * amount, lab.A, lab.B);
            return shifted.ToColor();
        }

        public static Color Darken(this Color color, int amount = 1)
        {
            return color.ShiftLightness(amount);
        }

        public static Color Lighten(this Color color, int amount = 1)
        {
            return color.ShiftLightness(-amount);
        }

        /// <summary>
        /// Calculate relative luminance of color.
        /// https://www.w3.org/TR/WCAG21/#dfn-relative-luminance
        /// </summary>
        /// <param name="c">Color used for measurement.</param>
        /// <returns>The magnitude of relative luminance of color</returns>
        public static double RelativeLuminance(this Color c)
        {
            double Process(double s) =>
                s < 0.03928 ? s / 12.92 : Math.Pow((s + 0.055) / 1.055, 2.4);

            double dR = (double) c.R / 255,
                dG = (double) c.G / 255,
                dB = (double) c.B / 255;

            var r = Process(dR);
            var g = Process(dG);
            var b = Process(dB);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }

        /// <summary>
        /// Get color contrast between two colors
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>Maximum possible contrast value. E.g: contrast white and black is 21, then it return 21.</returns>
        public static double Contrast(this Color a, Color b)
        {
            var l1 = RelativeLuminance(a) + 0.05;
            var l2 = RelativeLuminance(b) + 0.05;

            var ratio = l1 / l2;

            if (l2 > l1)
                ratio = l2 / l1;

            return ratio;
        }

        /// <summary>
        ///     Calculates the CIE76 distance between two colors.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static double Difference(this Color color, Color other)
        {
            var lab1 = color.ToLab();
            var lab2 = other.ToLab();

            return Math.Sqrt(Math.Pow(lab2.L - lab1.L, 2) +
                             Math.Pow(lab2.A - lab1.A, 2) +
                             Math.Pow(lab2.B - lab1.B, 2));
        }

        // https://github.com/LeaVerou/contrast-ratio
        private static Color AlgorithmContrastColor(Color backColor, Color a, Color b, double ratio = 4.5)
        {
            var contrast1 = Contrast(backColor, a);
            var contrast2 = Contrast(backColor, b);

            Color result;

            switch (contrast1 >= ratio)
            {
                case true:
                    result = contrast1 > contrast2 ? a : b;
                    break;
                default:
                    result = contrast2 > contrast1 ? b : a;
                    break;
            }

            return result;
        }
    }
}