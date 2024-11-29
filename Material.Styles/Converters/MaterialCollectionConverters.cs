using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters;

public static class MaterialCollectionConverters {
    public static FirstFromCollectionConverters First { get; } = new();

    public static IsNotNullOrEmptyCollectionConverters IsNotNullOrNullOrEmpty { get; } = new();

    public class FirstFromCollectionConverters : IValueConverter {
        public static FirstFromCollectionConverters Instance { get; } = new();
        /// <inheritdoc />
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            var enumerable = (IEnumerable<object>)value!;
            var first = enumerable?.FirstOrDefault();
            return first ?? BindingOperations.DoNothing;
        }

        /// <inheritdoc />
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }

    public class IsNotNullOrEmptyCollectionConverters : IValueConverter {
        public static FirstFromCollectionConverters Instance { get; } = new();
        /// <inheritdoc />
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            var enumerable = (IEnumerable<object>?)value;
            return enumerable?.Any() ?? false;
        }
        /// <inheritdoc />
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}