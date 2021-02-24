using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Dialog.Converters
{
    public class PadNumbersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ushort v:
                    return v.ToString("D2");
                case short v:
                    return v.ToString("D2");
                default:
                    return "00";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}