using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters;

public class BottomMarginConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double bottom = value is double d ? d : 0;
        return new Thickness(0, 0, 0, bottom);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => BindingOperations.DoNothing;
}