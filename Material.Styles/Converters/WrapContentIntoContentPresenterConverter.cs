using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    internal class WrapContentIntoContentPresenterConverter : IValueConverter {
        public static WrapContentIntoContentPresenterConverter Instance { get; } = new WrapContentIntoContentPresenterConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is IControl ? value : new ContentPresenter() { Content = value };

        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}