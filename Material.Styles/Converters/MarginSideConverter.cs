using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters;

public class MarginSideConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double v = value is double d ? d : 0;
        string? side = parameter?.ToString()?.ToLowerInvariant();

        return side switch {
            "top" => new Thickness(0, v, 0, 0),
            "bottom" => new Thickness(0, 0, 0, v),
            "left" => new Thickness(0, 0, v, 0),
            "right" => new Thickness(v, 0, 0, 0),
            _ => new Thickness(0)   // fallback
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => BindingOperations.DoNothing;
}