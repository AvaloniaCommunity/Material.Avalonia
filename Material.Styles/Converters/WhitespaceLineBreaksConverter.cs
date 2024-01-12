using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters;

public class WhitespaceLineBreaksConverter : IValueConverter {
    private static Dictionary<int, string> _cached = new();
    public static WhitespaceLineBreaksConverter Instance { get; } = new();
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        var lines = (int)value!;
        switch (lines) {
            case 0:
                return BindingOperations.DoNothing;
            case 1:
                return " ";
        }

        if (_cached.TryGetValue(lines, out var cached)) return cached;

        var s = new string('\n', lines - 1);
        _cached[lines] = s;

        return s;
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}