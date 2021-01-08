using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Material.Styles.Converters
{
    public class SliderTickMarkCreator : IMultiValueConverter
    {
        private const double strokeWidth = 1;
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            double x = strokeWidth, y = 0, f = 1;
            if (!(values[0] is Line line)) return createVectorList(x, y);
            if (!(values[1] is double min)) return createVectorList(x, y);
            if (!(values[2] is double max)) return createVectorList(x, y);
            if (!(values[3] is double freq)) return createVectorList(x, y);
            f = freq;
            if (f == 0.0)
                f = 1;

            /* var w = max - min;
             var c = w / f;
             //y = (line.Width / ((c - strokeWidth))); 
             //y = ((line.Width / c) - strokeWidth / line.StrokeThickness);
             //var s = line.Width / w; 
             var ba = line.Width / c;
             y = (ba - strokeWidth);
            // y = (y * s);*/
            var w = max - min;
            var c = (w / f) + 1;
            var p = line.Width / c;
            var a = p - strokeWidth * line.StrokeThickness;
            y = a / 2;
            var b = a / c;
            y += (b / 1.8);

            return createVectorList(x, y);
        }

        public object ConvertBack(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private AvaloniaList<double> createVectorList(double x, double y) => new AvaloniaList<double>(x, y);
    }
}
