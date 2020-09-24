using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;

namespace MaterialXamlToolKit.Avalonia.Assists {
    public static class ToggleSwitchAssist {
        public static AvaloniaProperty<SolidColorBrush> SwitchTrackOnBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleSwitch, SolidColorBrush>(
            "SwitchTrackOnBackground", typeof(ToggleSwitchAssist));

        public static void SetSwitchTrackOnBackground(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(SwitchTrackOnBackgroundProperty, value);
        }

        public static SolidColorBrush GetSwitchTrackOnBackground(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(SwitchTrackOnBackgroundProperty);
        }

        public static AvaloniaProperty<SolidColorBrush> SwitchTrackOffBackgroundProperty = AvaloniaProperty.RegisterAttached<ToggleSwitch, SolidColorBrush>(
            "SwitchTrackOffBackground", typeof(ToggleSwitchAssist));

        public static void SetSwitchTrackOffBackground(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(SwitchTrackOffBackgroundProperty, value);
        }

        public static SolidColorBrush GetSwitchTrackOffBackground(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(SwitchTrackOffBackgroundProperty);
        }
    }
}