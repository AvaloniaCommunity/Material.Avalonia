using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class AutoCompleteBoxAssist {
        public static readonly AttachedProperty<bool> UseFloatingPlaceholderProperty =
            AvaloniaProperty.RegisterAttached<AutoCompleteBox, bool>(
                "UseFloatingPlaceholder", typeof(AutoCompleteBox));

        public static void SetUseFloatingPlaceholder(AvaloniaObject element, bool value) =>
            element.SetValue(UseFloatingPlaceholderProperty, value);

        public static bool UseUseFloatingPlaceholder(AvaloniaObject element) =>
            element.GetValue(UseFloatingPlaceholderProperty);
    }
}