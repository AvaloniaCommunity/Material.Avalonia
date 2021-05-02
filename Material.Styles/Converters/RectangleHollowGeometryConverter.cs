using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Material.Styles.Converters
{
    /// <summary>
    /// Converter for creating rectangle geometry with hollow. Basically used for Outline TextBox.
    /// </summary>
    public class RectangleHollowGeometryConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness t = new Thickness(4, 0);

            Rect main = Rect.Empty;
            Rect hollow = Rect.Empty;

            StreamGeometry result = null;
            
            try
            {
                Point s = new Point(1, 1);

                if ((values[0] is Rect mainR))
                {
                    main = mainR;
                }
                if ((values[1] is Rect hollowR))
                {
                    hollow = hollowR;
                }
                if ((values[2] is TransformedBounds tb))
                {
                    s = new Point(tb.Transform.M11, tb.Transform.M22);
                }

                // Base zone
                var m0 = main.TopLeft;
                var m1 = main.TopRight;
                var m2 = main.BottomRight;
                var m3 = main.BottomLeft;

                // Hollow zone
                var h0 = hollow.TopLeft;
                var h1 = hollow.TopRight;
                var h2 = hollow.BottomRight;
                var h3 = hollow.BottomLeft;

                // Limiter
                var lL = main.Left + t.Left;
                var lR = main.Right - t.Left;

                var str = $"M {m0.X} {m0.Y} " +
                          $"L {m1.X} {m1.Y} " +
                          $"L {m2.X} {m2.Y} " +
                          $"L {m3.X} {m3.Y} z " +
                          $"M {Math.Max(h0.X * s.X, lL)} {h0.Y * s.Y} " +
                          $"L {Math.Min(h1.X * s.X + 4, lR)} {h1.Y * s.Y} " +
                          $"L {Math.Min(h1.X * s.X + 4, lR)} {h2.Y * s.Y} " +
                          $"L {Math.Max(h3.X * s.X, lL)} {h3.Y * s.Y} z ";

                str = str.Replace(",", ".");
                
                result = StreamGeometry.Parse(str);
            }
            catch(Exception e)
            {
                var m0 = main.TopLeft;
                var m1 = main.TopRight;
                var m2 = main.BottomRight;
                var m3 = main.BottomLeft;
                
                result = StreamGeometry.Parse($"M {m0.X} {m0.Y} " +
                                               $"L {m1.X} {m1.Y} " +
                                               $"L {m2.X} {m2.Y} " +
                                               $"L {m3.X} {m3.Y} z ");
            }

            return result;
        }
    }
}