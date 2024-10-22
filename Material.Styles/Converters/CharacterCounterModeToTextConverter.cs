using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Material.Styles.Assists;

namespace Material.Styles.Converters;

public class CharacterCounterModeToTextConverter : IMultiValueConverter {
    public static CharacterCounterModeToTextConverter Instance { get; } = new();
    /// <inheritdoc />
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture) {
        if (values.Count is not 3) throw new InvalidOperationException("This converter should be used with 3 values. [0] - CharacterCounterMode, [1] - Text, [2] - MaxLength");

        var mode = (CharacterCounterMode)values[0]!;
        var textLength = (float)((string?)values[1] ?? string.Empty).Length;
        var maxLength = (int?)values[2];
        if (maxLength is 0) maxLength = null;

        return mode switch {
            CharacterCounterMode.Hidden                                                                             => null,
            CharacterCounterMode.OnlyCounter                                                                        => textLength.ToString(CultureInfo.InvariantCulture),
            CharacterCounterMode.CounterSlashLimit when maxLength is not null                                       => $"{textLength} / {maxLength}",
            CharacterCounterMode.CounterSlashLimit when maxLength is null                                           => textLength.ToString(CultureInfo.InvariantCulture),
            CharacterCounterMode.OnlyLimit when maxLength is not null                                               => maxLength.ToString(),
            CharacterCounterMode.RemainingAlways when maxLength is not null                                         => (maxLength - textLength).ToString(),
            CharacterCounterMode.RemainingIfCloseToLimit when maxLength is not null && textLength / maxLength > 0.8 => (maxLength - textLength).ToString(),
            _                                                                                                       => null
        };
    }
}