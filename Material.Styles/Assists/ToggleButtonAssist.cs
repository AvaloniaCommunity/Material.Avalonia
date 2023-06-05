using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ToggleButtonAssist {
        public static readonly AvaloniaProperty<IBrush?> UncheckedForegroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleButton, IBrush?>(
                "UncheckedForeground", typeof(ToggleButtonAssist));

        public static void SetUncheckedForeground(AvaloniaObject element, IBrush? value) {
            element.SetValue(UncheckedForegroundProperty, value);
        }

        public static IBrush? GetUncheckedForeground(AvaloniaObject element) {
            return element.GetValue<IBrush?>(UncheckedForegroundProperty);
        }

        public static readonly AvaloniaProperty<IBrush?> UncheckedBackgroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleButton, IBrush?>(
                "UncheckedBackground", typeof(ToggleButtonAssist));

        public static void SetUncheckedBackground(AvaloniaObject element, IBrush? value) {
            element.SetValue(UncheckedBackgroundProperty, value);
        }

        public static IBrush? GetUncheckedBackground(AvaloniaObject element) {
            return element.GetValue<IBrush?>(UncheckedBackgroundProperty);
        }
    }
}
