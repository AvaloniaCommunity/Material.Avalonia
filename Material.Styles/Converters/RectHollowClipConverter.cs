using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Material.Styles.Converters.Parameters;

namespace Material.Styles.Converters
{
    /// <summary>
    /// Converter for creating rectangle geometry with hollow. Used for Outline TextBox.
    /// </summary>
    public class RectHollowClipConverter : IMultiValueConverter
    {
        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            double hOffset = 4, vOffset = -8;
            var t = new Thickness(2);

            if (parameter is RectHollowClipParameter param)
            {
                t = param.Margin;
                hOffset = param.Offset.X;
                vOffset = param.Offset.Y;
            }

            var main = Rect.Empty;
            var hollow = Rect.Empty;

            Geometry result;
            
            try
            {
                var s = new Point(1, 1);
                
                if (values[0] is Rect outer && values[1] is Rect inner)
                {
                    main = outer;
                    hollow = inner;
                }
                if (values.Count == 3 && values[2] is TransformOperations transform)
                {
                    if (Matrix.TryDecomposeTransform(transform.Value, out var d))
                        s = new Point(d.Scale.X, d.Scale.Y);
                }
                
                // Base zone
                var m0 = main.TopLeft;
                var m1 = main.BottomRight;
                
                // Hollow zone
                var h0 = Multiply(hollow.TopLeft - new Point(t.Left - hOffset, t.Top - vOffset), s);
                var h1 = Multiply(hollow.BottomRight + new Point(t.Right + hOffset, t.Bottom + vOffset), s);
                
                // Create geometry
                var outerGeometry = new StreamGeometry();
                var innerGeometry = new StreamGeometry();

                using (var ctx = outerGeometry.Open())
                {
                    ctx.BeginFigure(m0, true);
                    ctx.LineTo(new Point(m1.X, m0.Y));
                    ctx.LineTo(new Point(m1.X, m1.Y));
                    ctx.LineTo(new Point(m0.X, m1.Y));
                    ctx.EndFigure(true);
                }
                
                using (var ctx = innerGeometry.Open())
                {
                    ctx.BeginFigure(new Point(h0.X, h0.Y), true);
                    ctx.LineTo(new Point(h1.X, h0.Y));
                    ctx.LineTo(new Point(h1.X, h1.Y));
                    ctx.LineTo(new Point(h0.X, h1.Y));
                    ctx.EndFigure(true);
                }
                
                result = new CombinedGeometry(GeometryCombineMode.Xor, innerGeometry, outerGeometry);
            }
            catch
            {
                var m0 = main.TopLeft;
                var m1 = main.BottomRight;
                
                var outerGeometry = new StreamGeometry();

                using var ctx = outerGeometry.Open();
                
                ctx.BeginFigure(m0, true);
                ctx.LineTo(new Point(m1.X, m0.Y));
                ctx.LineTo(new Point(m1.X, m1.Y));
                ctx.LineTo(new Point(m0.X, m1.Y));
                ctx.EndFigure(true);
                
                result = outerGeometry;
            }

            return result;
        }
        
        private static Point Multiply(Point p, Point s)
        {
            return new Point(p.X * s.X, p.Y * s.Y);
        }
    }
}