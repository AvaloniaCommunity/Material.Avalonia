using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ToggleSwitchAssist {
        public static AvaloniaProperty<IBrush> SwitchTrackOnBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush>(
            "SwitchTrackOnBackground", typeof(ToggleSwitchAssist));

        public static AvaloniaProperty<IBrush> SwitchTrackOffBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush>(
            "SwitchTrackOffBackground", typeof(ToggleSwitchAssist));

        public static void SetSwitchTrackOnBackground(AvaloniaObject element, IBrush value) {
            element.SetValue(SwitchTrackOnBackgroundProperty, value);
        }

        public static IBrush GetSwitchTrackOnBackground(AvaloniaObject element) {
            return (IBrush) element.GetValue(SwitchTrackOnBackgroundProperty);
        }

        public static void SetSwitchTrackOffBackground(AvaloniaObject element, IBrush value) {
            element.SetValue(SwitchTrackOffBackgroundProperty, value);
        }

        public static IBrush GetSwitchTrackOffBackground(AvaloniaObject element) {
            return (IBrush) element.GetValue(SwitchTrackOffBackgroundProperty);
        }
    }
}