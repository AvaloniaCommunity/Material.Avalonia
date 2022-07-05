using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Dialog.Converters
{
    public class DateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = "ddd, MMM d";

            if (parameter is string s)
                format = s;

            if (value is DateTime)
            {
                var v = (DateTime) value;
                return v.ToString(format);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}