using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class DatePickerAssist {
        public static readonly AttachedProperty<string> DateTimeFormatProperty
            = AvaloniaProperty.RegisterAttached<DatePicker, string>("DateTimeFormat", typeof(DatePickerAssist));

        public static string GetDateTimeFormat(DatePicker element) {
            return element.GetValue(DateTimeFormatProperty);
        }

        /// <summary>
        /// Sets <see cref="DateTimeFormatProperty"/>
        /// </summary>
        /// <param name="element">Target DatePicker</param>
        /// <param name="value">Format string</param>
        /// <remarks>When not null then 
        /// <see cref="DatePicker.YearFormat"/>, 
        /// <see cref="DatePicker.YearVisible"/>,
        /// <see cref="DatePicker.DayFormat"/>, 
        /// <see cref="DatePicker.DayVisible"/>,
        /// <see cref="DatePicker.MonthFormat"/> and 
        /// <see cref="DatePicker.MonthVisible"/>
        /// are ignored
        /// </remarks>
        /// <example>"dddd, dd MMMM yyyy" will be displayed as "Friday, 29 May 2015"</example>
        public static void SetDateTimeFormat(DatePicker element, string value) {
            element.SetValue(DateTimeFormatProperty, value);
        }
    }
}
