using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class TimePickerAssist {
        public static readonly AttachedProperty<string> DateTimeFormatProperty =
            AvaloniaProperty.RegisterAttached<TimePicker, string>("DateTimeFormat", typeof(TimePickerAssist));

        public static readonly AttachedProperty<bool> CanSelectSecondsProperty =
            AvaloniaProperty.RegisterAttached<TimePicker, bool>("CanSelectSeconds", typeof(TimePickerAssist));

        public static string GetDateTimeFormat(TimePicker element) =>
            element.GetValue(DateTimeFormatProperty);

        /// <summary>
        /// Sets <see cref="DateTimeFormatProperty"/>
        /// </summary>
        /// <param name="element">Target <see cref="TimePicker"/></param>
        /// <param name="value">Format string</param>
        /// <example>"HH:mm:ss" will be displayed as "14:11:23"</example>
        public static void SetDateTimeFormat(TimePicker element, string value) =>
            element.SetValue(DateTimeFormatProperty, value);

        public static bool GetCanSelectSeconds(TimePicker element) {
            return element.GetValue(CanSelectSecondsProperty);
        }

        /// <summary>
        /// Sets <see cref="CanSelectSecondsProperty"/>
        /// </summary>
        /// <param name="element">Target <see cref="TimePicker"/></param>
        /// <param name="value">Is seconds can be selected with picker</param>
        public static void SetCanSelectSeconds(TimePicker element, bool value) {
            element.SetValue(CanSelectSecondsProperty, value);
        }
    }
}
