using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Material.Styles.Converters
{
    /// <summary>
    /// Converter for NavigationDrawer use.
    /// </summary>
    public class MarginCreator : IMultiValueConverter
    {
        public const double Offset = -8;

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values[0] is double left)) 
                return createMargin(left: -left + Offset);
            if ((values[1] is double up)) 
                return createMargin(up: -up + Offset);
            if ((values[2] is double right)) 
                return createMargin(right: -right + Offset);
            if ((values[3] is double down)) 
                return createMargin(down: -down + Offset);
            return createMargin();
        }

        public static Thickness createMargin(double left = 0, double up = 0, double right = 0, double down = 0) => new Thickness(left, up, right, down);
    }
}
