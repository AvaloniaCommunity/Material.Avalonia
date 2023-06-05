using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    public sealed class ProgressBarIntermediateOffsetConverter : IValueConverter {
        /// <inheritdoc />
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            return (double)value! * System.Convert.ToDouble(parameter, NumberFormatInfo.InvariantInfo);
        }
        /// <inheritdoc />
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
