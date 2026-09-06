using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Material.Avalonia.Demo.Converters;

public sealed class TableViewColumnWidthConverter : IValueConverter {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value is true && parameter is string stringParameter &&
               double.TryParse(stringParameter, NumberStyles.Number, CultureInfo.InvariantCulture, out var baseWidth)
            ? new GridLength(baseWidth, GridUnitType.Star)
            : value is false && parameter is string pixelParameter &&
              double.TryParse(pixelParameter, NumberStyles.Number, CultureInfo.InvariantCulture, out var pixelWidth)
                ? new GridLength(pixelWidth * 100, GridUnitType.Pixel)
                : AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}