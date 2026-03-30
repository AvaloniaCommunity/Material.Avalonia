using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class AutoCompleteBoxAssist {
        public static readonly AttachedProperty<bool> UseFloatingWatermarkProperty =
            AvaloniaProperty.RegisterAttached<AutoCompleteBox, bool>(
                "UseFloatingWatermark", typeof(AutoCompleteBox), false);

        public static void SetUseFloatingWatermark(AvaloniaObject element, bool value) =>
            element.SetValue(UseFloatingWatermarkProperty, value);

        public static bool GetUseFloatingWatermark(AvaloniaObject element) =>
            element.GetValue(UseFloatingWatermarkProperty);
    }
}