using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Material.Styles.Enums;

namespace Material.Styles.Converters;

public class AmPmRangeConverter : IValueConverter {
    public static AmPmRangeConverter MinimumInstance { get; } = new() { IsMinimum = true };
    public static AmPmRangeConverter MaximumInstance { get; } = new() { IsMinimum = false };
    public bool IsMinimum { get; set; }
    /// <inheritdoc />
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is TimeFormat format) {
            return format switch {
                TimeFormat.TwelveHour when IsMinimum      => 1,
                TimeFormat.TwelveHour when !IsMinimum     => 12,
                TimeFormat.TwentyFourHour when IsMinimum  => 0,
                TimeFormat.TwentyFourHour when !IsMinimum => 23,
                _                                         => throw new ArgumentOutOfRangeException()
            };
        }
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}
