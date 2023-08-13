using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Dialog.Converters {
    public class HourStringConverter : IValueConverter {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            if (value is ushort v) {
                if (v == 0) {
                    return 12.ToString();
                }

                return v.ToString();
            }

            return 0.ToString();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
