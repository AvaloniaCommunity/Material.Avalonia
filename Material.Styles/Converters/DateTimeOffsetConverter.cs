using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Material.Styles.Converters {
    /// <summary>
    /// Used for convert <see cref="DateTimeOffset"/> to <see cref="DateTime"/>, or reveal operation.
    /// </summary>
    public class DateTimeOffsetConverter : IValueConverter {
        /// <summary>
        /// <p>Single instance of this converter. Recommend use it rather than create a new one.</p>
        /// <p>Usage in avaloniaUI: Converter='{x:Static converters:DateTimeOffsetConverter.Instance}'</p>
        /// </summary>
        public static DateTimeOffsetConverter Instance { get; } = new();

        /// <summary>
        /// Convert <see cref="DateTimeOffset"/> to <see cref="DateTime"/>
        /// </summary>
        /// <param name="value"><see cref="DateTimeOffset"/> instance.</param>
        /// <param name="targetType">Ignored</param>
        /// <param name="parameter">Ignored</param>
        /// <param name="culture">Ignored</param>
        /// <returns>Returns <see cref="DateTime"/> or value object if not <see cref="DateTimeOffset"/>.</returns>
        public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture) {
            if (value is DateTimeOffset offset)
                return offset.Date;

            return value;
        }

        /// <summary>
        /// Convert <see cref="DateTime"/> to <see cref="DateTimeOffset"/>
        /// </summary>
        /// <param name="value"><see cref="DateTime"/> instance.</param>
        /// <param name="targetType">Ignored</param>
        /// <param name="parameter">Ignored</param>
        /// <param name="culture">Ignored</param>
        /// <returns>Returns <see cref="DateTimeOffset"/> or value object if not <see cref="DateTime"/>.</returns>
        public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture) {
            if (value is DateTime dateTime)
                return new DateTimeOffset(dateTime);

            return value;
        }
    }
}
