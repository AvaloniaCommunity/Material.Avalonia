using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters; 

public class IntParseConverter : IValueConverter {
    public static IntParseConverter Instance { get; } = new();
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is string s) {
            return int.Parse(s);
        }
        return BindingOperations.DoNothing;
    }
    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is int i) {
            return i.ToString();
        }
        return BindingOperations.DoNothing;
    }
}
