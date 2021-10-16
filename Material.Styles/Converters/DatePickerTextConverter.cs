using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    public class DatePickerTextConverter : IMultiValueConverter {
        public static DatePickerTextConverter Instance { get; } = new DatePickerTextConverter();
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture) {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable once HeuristicUnreachableCode
            try {
                return values[0] is UnsetValueType || values[0] == null ? "Not selected" : ((DateTimeOffset)values[0]).ToString((string)values[1]);
            }
            catch (Exception) {
                return BindingOperations.DoNothing;
            }
        }
    }
}