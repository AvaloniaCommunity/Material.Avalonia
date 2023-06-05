using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    public class MaximizeCornerRadiusConverter : IValueConverter {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            if (value is Rect r)
                return new CornerRadius(r.Left / 2,
                    r.Top / 2,
                    r.Right / 2,
                    r.Bottom / 2);

            // Works only for Skia backend rendering
            // DX2D or other backends might works abnormally
            return new CornerRadius(double.MaxValue);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
