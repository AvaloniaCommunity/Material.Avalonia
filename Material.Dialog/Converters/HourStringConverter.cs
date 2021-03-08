using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Dialog.Converters
{
    public class HourStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as ushort?;
            if (v.HasValue)
            {
                if (v.Value == 0)
                {
                    return 12.ToString();
                }

                return v.Value.ToString();
            }

            return 0.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}