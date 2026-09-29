using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Material.Styles.Assists;

/// <summary>
///     Uses an overlay for Material button menus on macOS to avoid stalls when a native Metal popup opens.
/// </summary>
public static class MenuFlyoutAssist {
    public static readonly AttachedProperty<bool> UseOverlayOnMacOSProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("UseOverlayOnMacOS", typeof(MenuFlyoutAssist));

    static MenuFlyoutAssist() {
        UseOverlayOnMacOSProperty.Changed.AddClassHandler<Button>((button, _) => Configure(button, button.Flyout));
        UseOverlayOnMacOSProperty.Changed.AddClassHandler<SplitButton>((button, _) => Configure(button, button.Flyout));
        Button.FlyoutProperty.Changed.AddClassHandler<Button>((button, e) =>
            Configure(button, e.GetNewValue<FlyoutBase?>()));
        SplitButton.FlyoutProperty.Changed.AddClassHandler<SplitButton>((button, e) =>
            Configure(button, e.GetNewValue<FlyoutBase?>()));
    }

    public static bool GetUseOverlayOnMacOS(Control control) {
        return control.GetValue(UseOverlayOnMacOSProperty);
    }

    public static void SetUseOverlayOnMacOS(Control control, bool value) {
        control.SetValue(UseOverlayOnMacOSProperty, value);
    }

    private static void Configure(Control control, FlyoutBase? flyout) {
        if (OperatingSystem.IsMacOS() && GetUseOverlayOnMacOS(control) && flyout is MenuFlyout menuFlyout &&
            !menuFlyout.Popup.IsSet(Popup.ShouldUseOverlayLayerProperty)) {
            menuFlyout.Popup.ShouldUseOverlayLayer = true;
        }
    }
}