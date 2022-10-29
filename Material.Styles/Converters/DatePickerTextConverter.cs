using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters
{
    public class DatePickerTextConverter : IMultiValueConverter
    {
        public static DatePickerTextConverter Instance { get; } = new();

        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                return values[0] is UnsetValueType || values[0] == null
                    ? "Not selected"
                    : values[0] is DateTimeOffset offset
                        ? offset.ToString(values[1] as string)
                        : BindingOperations.DoNothing;
            }
            catch
            {
                return BindingOperations.DoNothing;
            }
        }
    }
}