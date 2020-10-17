using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ToggleButtonAssist {
        public static AvaloniaProperty<SolidColorBrush> UncheckedForegroundProperty = AvaloniaProperty.RegisterAttached<ToggleButton, SolidColorBrush>(
            "UncheckedForeground", typeof(ToggleButtonAssist));
        
        public static void SetUncheckedForeground(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(UncheckedForegroundProperty, value);
        }

        public static SolidColorBrush GetUncheckedForeground(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(UncheckedForegroundProperty);
        }
        
        public static AvaloniaProperty<SolidColorBrush> UncheckedBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleButton, SolidColorBrush>(
            "UncheckedBackground", typeof(ToggleButtonAssist));
        
        public static void SetUncheckedBackground(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(UncheckedBackgroundProperty, value);
        }

        public static SolidColorBrush GetUncheckedBackground(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(UncheckedBackgroundProperty);
        }
    }
}