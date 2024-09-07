using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Transformation;

namespace Material.Dialog.Converters {
    public class StringToTransformConverter : IValueConverter {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            var stringValue = value?.ToString();
            if (stringValue == null)
                return TransformOperation.Identity;
            return TransformOperations.Parse(stringValue);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
