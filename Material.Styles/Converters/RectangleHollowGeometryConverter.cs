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
            double offsetL = 4, offsetR = 4;
            Thickness t = new Thickness(4, 0);

            if (parameter != null)
            {
                try
                {
                    t = Thickness.Parse(parameter.ToString());
                }
                catch
                {
                    // ignored
                }
            }

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
                var m1 = main.BottomRight;

                // Hollow zone
                var h0 = hollow.TopLeft + new Point(offsetL - t.Left, 0);
                var h1 = hollow.BottomRight + new Point(offsetR + t.Right, 0);

                // Limiter
                var lL = main.Left - t.Left + offsetL;
                var lR = main.Right + t.Right + offsetR;

                var str = $"M {m0.X} {m0.Y} " +
                          $"L {m1.X} {m0.Y} " +
                          $"L {m1.X} {m1.Y} " +
                          $"L {m0.X} {m1.Y} z " +
                          $"M {Math.Max(h0.X * s.X, lL)} {h0.Y * s.Y} " +
                          $"L {Math.Min(h1.X * s.X, lR)} {h0.Y * s.Y} " +
                          $"L {Math.Min(h1.X * s.X, lR)} {h1.Y * s.Y} " +
                          $"L {Math.Max(h0.X * s.X, lL)} {h1.Y * s.Y} z ";

                str = str.Replace(",", ".");
                
                result = StreamGeometry.Parse(str);
            }
            catch(Exception e)
            {
                var m0 = main.TopLeft;
                var m1 = main.BottomRight;
                
                result = StreamGeometry.Parse($"M {m0.X} {m0.Y} " +
                                               $"L {m1.X} {m0.Y} " +
                                               $"L {m1.X} {m1.Y} " +
                                               $"L {m0.X} {m1.Y} z ");
            }

            return result;
        }
    }
}