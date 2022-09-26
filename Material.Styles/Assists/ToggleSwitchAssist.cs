using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

// IDE will always warn you about this, but those members are actually used by the XAML compiler and AvaloniaUI.
// ReSharper disable UnusedMember.Global

namespace Material.Styles.Assists
{
    public static class ToggleSwitchAssist
    {
        #region Switch track background (Checked state)

        public static readonly AvaloniaProperty<IBrush?> SwitchTrackOnBackgroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush?>(
                "SwitchTrackOnBackground", typeof(ToggleSwitchAssist));

        public static IBrush? GetSwitchTrackOnBackground(AvaloniaObject element) =>
            element.GetValue<IBrush?>(SwitchTrackOnBackgroundProperty);

        public static void SetSwitchTrackOnBackground(AvaloniaObject element, IBrush? value) =>
            element.SetValue(SwitchTrackOnBackgroundProperty, value);

        #endregion

        #region Switch track background (Unchecked state)

        public static readonly AvaloniaProperty<IBrush?> SwitchTrackOffBackgroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush?>(
                "SwitchTrackOffBackground", typeof(ToggleSwitchAssist));

        public static IBrush? GetSwitchTrackOffBackground(AvaloniaObject element) =>
            element.GetValue<IBrush?>(SwitchTrackOffBackgroundProperty);

        public static void SetSwitchTrackOffBackground(AvaloniaObject element, IBrush? value) =>
            element.SetValue(SwitchTrackOffBackgroundProperty, value);

        #endregion

        #region Switch thumb background (Checked state)

        public static readonly AvaloniaProperty<IBrush?> SwitchThumbOnBackgroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush?>(
                "SwitchThumbOnBackground", typeof(ToggleSwitchAssist));

        public static IBrush? GetSwitchThumbOnBackground(AvaloniaObject element) =>
            element.GetValue<IBrush?>(SwitchThumbOnBackgroundProperty);

        public static void SetSwitchThumbOnBackground(AvaloniaObject element, IBrush? value) =>
            element.SetValue(SwitchThumbOnBackgroundProperty, value);

        #endregion

        #region Switch thumb background (Unchecked state)

        public static readonly AvaloniaProperty<IBrush?> SwitchThumbOffBackgroundProperty =
            AvaloniaProperty.RegisterAttached<ToggleSwitch, IBrush?>(
                "SwitchThumbOffBackground", typeof(ToggleSwitchAssist));

        public static IBrush? GetSwitchThumbOffBackground(AvaloniaObject element) =>
            element.GetValue<IBrush?>(SwitchThumbOffBackgroundProperty);

        public static void SetSwitchThumbOffBackground(AvaloniaObject element, IBrush? value) =>
            element.SetValue(SwitchThumbOffBackgroundProperty, value);

        #endregion
    }
}