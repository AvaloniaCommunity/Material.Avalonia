using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters; 

public class Int32ToDecimalConverter : IValueConverter {
    public static Int32ToDecimalConverter Instance { get; } = new();
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value switch {
            int i => (decimal)i,
            null  => null,
            _     => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value switch {
            decimal d => (int)d,
            null      => null,
            _         => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
