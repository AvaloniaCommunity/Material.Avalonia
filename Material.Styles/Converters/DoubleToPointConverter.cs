using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Material.Styles.Converters
{
    /// <summary>
    /// Utility for slider stroke line.
    /// </summary>
    public class DoubleToPointConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new BrushRoundConverter(); 

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double d)) return new Point(0,0);
            return new Point(d, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
