using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Transformation;

namespace Material.Styles.Converters {
    /// <summary>
    /// Converter for creating rectangle geometry with hollow. Used for Outline TextBox.
    /// </summary>
    public class RectHollowClipConverter : IMultiValueConverter {
        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
            var margin = new Thickness(2);

            if (parameter is Thickness param) margin = param;

            Rect main = default;
            Rect hollow = default;

            try {
                var s = new Point(1, 1);

                if (values[0] is Rect outer && values[1] is Rect inner) {
                    main = outer;
                    hollow = inner;
                }

                if (values.Count > 2
                 && values[2] is TransformOperations transform
                 && Matrix.TryDecomposeTransform(transform.Value, out var d))
                    s = new Point(d.Scale.X, d.Scale.Y);

                var hollowTransformed = new Rect(
                    hollow.TopLeft.X - margin.Left,
                    hollow.TopLeft.Y - margin.Top,
                    hollow.Size.Width * s.X + margin.Left + margin.Right,
                    hollow.Size.Height * s.Y + margin.Top + margin.Bottom);

                // Create geometry
                var outerGeometry = new StreamGeometry();
                var innerGeometry = new StreamGeometry();

                using (var ctx = outerGeometry.Open()) {
                    ctx.BeginFigure(main.TopLeft, true);
                    ctx.LineTo(main.TopRight);
                    ctx.LineTo(main.BottomRight);
                    ctx.LineTo(main.BottomLeft);
                    ctx.EndFigure(true);
                }

                using (var ctx = innerGeometry.Open()) {
                    ctx.BeginFigure(hollowTransformed.TopLeft, true);
                    ctx.LineTo(hollowTransformed.TopRight);
                    ctx.LineTo(hollowTransformed.BottomRight);
                    ctx.LineTo(hollowTransformed.BottomLeft);
                    ctx.EndFigure(true);
                }

                return new CombinedGeometry(GeometryCombineMode.Xor, innerGeometry, outerGeometry);
            }
            catch {
                var m0 = main.TopLeft;
                var m1 = main.BottomRight;

                var outerGeometry = new StreamGeometry();

                using var ctx = outerGeometry.Open();

                ctx.BeginFigure(m0, true);
                ctx.LineTo(new Point(m1.X, m0.Y));
                ctx.LineTo(new Point(m1.X, m1.Y));
                ctx.LineTo(new Point(m0.X, m1.Y));
                ctx.EndFigure(true);

                return outerGeometry;
            }
        }
    }
}