using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace MaterialXamlToolKit.Avalonia.Converters {
    public class BrushRoundConverter : IValueConverter {
        public static readonly IValueConverter Instance = new BrushRoundConverter();
        public Brush HighValue { get; set; } = new SolidColorBrush(Brushes.White.Color);

        public Brush LowValue { get; set; } = new SolidColorBrush(Brushes.Black.Color);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush solidColorBrush)) return null;

            var color = solidColorBrush.Color;

            var brightness = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;

            return brightness < 123 ? LowValue : HighValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}