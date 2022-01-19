using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ToggleButtonAssist {
        public static AvaloniaProperty<IBrush> UncheckedForegroundProperty = AvaloniaProperty.RegisterAttached<ToggleButton, IBrush>(
            "UncheckedForeground", typeof(ToggleButtonAssist));
        
        public static void SetUncheckedForeground(AvaloniaObject element, IBrush value) {
            element.SetValue(UncheckedForegroundProperty, value);
        }

        public static IBrush GetUncheckedForeground(AvaloniaObject element) {
            return (IBrush) element.GetValue(UncheckedForegroundProperty);
        }
        
        public static AvaloniaProperty<IBrush> UncheckedBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleButton, IBrush>(
            "UncheckedBackground", typeof(ToggleButtonAssist));
        
        public static void SetUncheckedBackground(AvaloniaObject element, IBrush value) {
            element.SetValue(UncheckedBackgroundProperty, value);
        }

        public static IBrush GetUncheckedBackground(AvaloniaObject element) {
            return (IBrush) element.GetValue(UncheckedBackgroundProperty);
        }
    }
}