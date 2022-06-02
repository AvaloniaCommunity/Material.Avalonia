using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Material.Styles.Converters.Parameters;

namespace Material.Styles.Converters
{
    public class MarginMultiplyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var param = parameter switch
            {
                MarginMultiplyParameter p => p,
                double m => new MarginMultiplyParameter
                {
                    BottomMultiplier = m, LeftMultiplier = m, RightMultiplier = m, TopMultiplier = m
                },
                _ => MarginMultiplyParameter.Default
            };

            var result = value switch
            {
                // If value is double primitive type
                double v when !double.IsNaN(v) => new Thickness(v * param.LeftMultiplier, v * param.TopMultiplier,
                    v * param.RightMultiplier, v * param.BottomMultiplier),
                
                // or value is 32-bit integer primitive type
                int i => new Thickness(i * param.LeftMultiplier, i * param.TopMultiplier, i * param.RightMultiplier,
                    i * param.BottomMultiplier),
                
                // or its unsupported type
                _ => Thickness.Parse("0")
            };

            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}