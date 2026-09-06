using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Material.Avalonia.Demo.Converters;

public sealed class GdpToBrushConverter : IValueConverter {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value is int gdp
            ? gdp switch {
                <= 5000 => new SolidColorBrush(Color.FromArgb(40, 255, 152, 0)),
                <= 10000 => new SolidColorBrush(Color.FromArgb(40, 255, 193, 7)),
                _ => new SolidColorBrush(Color.FromArgb(40, 76, 175, 80))
            }
            : AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}