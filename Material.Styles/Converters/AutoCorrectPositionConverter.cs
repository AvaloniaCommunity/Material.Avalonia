using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.VisualTree;

namespace Material.Styles.Converters
{
    public class AutoCorrectPositionConverter : IValueConverter
    {
        public static double DefaultOffsetY = 0;
        
        private static double GetOffLeft(Rect bounds, double offsetX) => offsetX;

        private static double GetOffRight(Rect bounds, double windowW, double offsetX) => offsetX + bounds.Width - windowW;

        private static Vector GetTranslate(Matrix m)
        {
            return Matrix.TryDecomposeTransform(m, out var decomposed) ?
                decomposed.Translate : Vector.Zero;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double offsetX = 0;
            if(value is TransformedBounds postTransformations)
            {
                var t = postTransformations.Transform;
                var b = postTransformations.Bounds;
                var c = postTransformations.Clip;
                
                var translate = GetTranslate(t);

                var left = GetOffLeft(b, translate.X);
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
            }
            return new TranslateTransform(offsetX, DefaultOffsetY);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}