using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Material.Styles.Assists.Mixins;

/// <summary>
/// Adds watermark promoted to label functionality to control classes.
/// 
/// Adds the ':watermark-as-label', ':has-label', ':has-watermark' class when the item is pressed.
/// </summary>
public static class WatermarkAsLabelMixin {
    /// <summary>
    /// Initializes a new instance of the <see cref="WatermarkAsLabelMixin"/> class.
    /// </summary>
    /// <typeparam name="TControl">The control type.</typeparam>
    public static void Attach<TControl>() where TControl : TemplatedControl {
        TextFieldAssist.WatermarkProperty.Changed.AddClassHandler<TControl>((x, _) => HandlePropertiesChanged(x));
        TextFieldAssist.UseFloatingLabelProperty.Changed.AddClassHandler<TControl>((x, _) => HandlePropertiesChanged(x));
        TextFieldAssist.LabelProperty.Changed.AddClassHandler<TControl>((x, _) => HandlePropertiesChanged(x));
    }

    private static void HandlePropertiesChanged<TControl>(TControl sender) where TControl : TemplatedControl {
        var watermark = sender.GetValue(TextFieldAssist.WatermarkProperty);
        var label = sender.GetValue(TextFieldAssist.LabelProperty);
        var useFloatingLabel = sender.GetValue(TextFieldAssist.UseFloatingLabelProperty);

        var useWatermarkAsLabel = useFloatingLabel && watermark is not null && label is null;
        ((IPseudoClasses)sender.Classes).Set(":watermark-as-label", useWatermarkAsLabel);
        ((IPseudoClasses)sender.Classes).Set(":has-label", label is not null || useWatermarkAsLabel);
        ((IPseudoClasses)sender.Classes).Set(":has-watermark", watermark is not null && !useWatermarkAsLabel);
    }
}