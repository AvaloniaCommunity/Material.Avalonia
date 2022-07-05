using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Material.Styles.Converters
{
    public class AutoCorrectPositionConverter : IMultiValueConverter
    {
        public static readonly Transform Empty = new MatrixTransform();

        public static double DefaultOffsetY = 0;

        private static double GetOffLeft(double offsetX) => offsetX;

        private static double GetOffRight(Rect bounds, double clipW, double offsetX)
        {
            var r = offsetX + bounds.Width;

            return Math.Max(0, r - clipW);
        }

        private static Vector GetTranslate(Matrix m)
        {
            return Matrix.TryDecomposeTransform(m, out var decomposed) ? decomposed.Translate : Vector.Zero;
        }

        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            double offsetX = 0;

            if (values.Count <= 1 || values.Count > 2)
                return Empty;

            if (values[1] is not Rect clip)
                return Empty;

            if (values[0] is not TransformedBounds postTransformations)
                return Empty;

            var t = postTransformations.Transform;
            var b = postTransformations.Bounds;
            var c = clip;

            var translate = GetTranslate(t);

            var left = GetOffLeft(translate.X);
            var right = GetOffRight(b, c.Width, translate.X);

            if (left < 0)
            {
                offsetX = -left;
                //_prevCorrect = new Vector(offsetX, DefaultOffsetY);
            }
            else if (right > 0)
            {
                offsetX = -right;
                // _prevCorrect = new Vector(offsetX, DefaultOffsetY);
            }

            return new TranslateTransform(offsetX, DefaultOffsetY);
        }
    }
}