using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters;

public class RightMarginConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double right = value is double d ? d : 0;
        return new Thickness(0, 0, right, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => BindingOperations.DoNothing;
}