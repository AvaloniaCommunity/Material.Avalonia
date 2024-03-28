using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists.Internal;

/// <summary>
/// Contains some internal properties for Material.Avalonia
/// </summary>
/// <remarks>
/// You probably SHOULDN'T USE THIS
/// </remarks>
public static class MaterialInternalAttachedProperties {
    public static readonly AttachedProperty<bool> UseClippingForBorderProperty
        = AvaloniaProperty.RegisterAttached<TextBox, bool>("UseClippingForBorder", typeof(MaterialInternalAttachedProperties), inherits: true);

    public static bool GetUseClippingForBorder(TextBox element) {
        return element.GetValue(UseClippingForBorderProperty);
    }

    public static void SetUseClippingForBorder(TextBox element, bool value) {
        element.SetValue(UseClippingForBorderProperty, value);
    }
}