using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Material.Styles.Enums;

namespace Material.Styles.Converters;

public class TimePickerTextConverter : IMultiValueConverter {
    public static TimePickerTextConverter Instance { get; } = new();
    /// <inheritdoc />
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
        if (values[0] is not TimeSpan timeSpan)
            return string.Empty;

        var formatString = values[3] as string;
        if (string.IsNullOrEmpty(formatString)) {
            var timeFormat = (string)values[1]! == "12HourClock" ? TimeFormat.TwelveHour : TimeFormat.TwentyFourHour;
            var hasSeconds = (bool)values[2]!;

            formatString = timeFormat switch {
                TimeFormat.TwelveHour when !hasSeconds     => "hh:mm tt",
                TimeFormat.TwelveHour when hasSeconds      => "hh:mm:ss tt",
                TimeFormat.TwentyFourHour when !hasSeconds => "HH:mm",
                TimeFormat.TwentyFourHour when hasSeconds  => "HH:mm:ss",
                _                                          => throw new ArgumentOutOfRangeException()
            };
        }
        
        return DateTime.MinValue.Add(timeSpan).ToString(formatString, CultureInfo.InvariantCulture);
    }
}
