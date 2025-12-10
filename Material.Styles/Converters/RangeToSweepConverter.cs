using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

// ReSharper disable HeapView.BoxingAllocation

namespace Material.Styles.Converters
{
    public class RangeToSweepConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            double min = 0, max = 100, val = 0;

            for (var i = 0; i < values.Count; i++)
            {
                if (values[i] == null)
                    return null;
            }

            if (values[0] is double value)
                val = value;

            if (values[1] is double minimum)
                min = minimum;

            if (values[2] is double maximum)
                max = maximum;

            // normalize values so 'min' is 0
            var normMin = min - min;
            var normVal = val - min;
            var normMax = max - min;

            var m = normMax - normMin;
            return normVal / m * 360;
        }
    }
}