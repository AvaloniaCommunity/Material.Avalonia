using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.VisualTree;

namespace Material.Styles.Converters
{
    public class AutoCorrectPositionConverter : IValueConverter
    {
        public static double DefaultOffsetY = 0;
        
        private static double GetOffLeft(Rect bounds, double offsetX) => offsetX;

        private static double GetOffRight(Rect bounds, double windowW, double offsetX) => offsetX + (bounds.Width) - windowW;

        private static Vector GetTranslate(TransformedBounds bounds)
        {
            return new Vector(bounds.Transform.M31, bounds.Transform.M32);
        }

        private Vector _prevCorrect = Vector.One;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var transformedBounds = value as TransformedBounds?;
            
            double offsetX = 0;
            if (transformedBounds.HasValue)
            {
                var bounds = transformedBounds.Value;
                
                var translate = GetTranslate(bounds);

                var left = GetOffLeft(bounds.Bounds, translate.X - _prevCorrect.X);
                var right = GetOffRight(bounds.Bounds, bounds.Clip.Width, translate.X- _prevCorrect.X);

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