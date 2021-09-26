using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    public class DateTimeToOffsetConverter : IValueConverter {
        public static DateTimeToOffsetConverter Instance { get; } = new DateTimeToOffsetConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTimeOffset offset) {
                return offset.Date;
            }
            
            return value;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTime dateTime) {
                return new DateTimeOffset(dateTime);
            }
            
            return value;
        }
    }
}