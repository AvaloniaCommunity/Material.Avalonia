using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters
{
    /// <summary>
    /// Converter for NavigationDrawer use. It create Margin by values.
    /// </summary>
    public class MarginCreator : IMultiValueConverter
    {
        public const double Offset = -8;

        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values[0] is double left)
                return CreateMargin(left: -left + Offset);
            
            if (values[1] is double up)
                return CreateMargin(up: -up + Offset);
            
            if (values[2] is double right)
                return CreateMargin(right: -right + Offset);
            
            if (values[3] is double down)
                return CreateMargin(down: -down + Offset);
            
            return CreateMargin();
        }

        private static Thickness CreateMargin(double left = 0, double up = 0, double right = 0, double down = 0) =>
            new(left, up, right, down);
    }
}