using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Material.Styles.Assists.Mixins;

namespace Material.Styles.Assists;

public static class TextFieldAssist {
    public static readonly AvaloniaProperty<string?> HintsProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, string?>("Hints", typeof(TextFieldAssist));

    public static readonly AvaloniaProperty<string?> LabelProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, string?>("Label", typeof(TemplatedControl));

    public static readonly AttachedProperty<string?> WatermarkProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, string?>("Watermark", typeof(TextFieldAssist));

    public static readonly AttachedProperty<bool> UseFloatingLabelProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, bool>("UseFloatingLabel", typeof(TextFieldAssist));
    static TextFieldAssist() {
        WatermarkAsLabelMixin.Attach<TextBox>();
    }

    public static void SetHints(TemplatedControl element, string? value) {
        element.SetValue(HintsProperty, value);
    }

    public static string? GetHints(TemplatedControl element) {
        return element.GetValue<string?>(HintsProperty);
    }

    public static void SetLabel(TemplatedControl element, string? value) {
        element.SetValue(LabelProperty, value);
    }

    public static string? GetLabel(TemplatedControl element) {
        return element.GetValue<string?>(LabelProperty);
    }

    public static string? GetWatermark(TemplatedControl element) {
        return element.GetValue(WatermarkProperty);
    }

    public static void SetWatermark(TemplatedControl element, string? value) {
        element.SetValue(WatermarkProperty, value);
    }

    public static bool GetUseFloatingLabel(TemplatedControl element) {
        return element.GetValue(UseFloatingLabelProperty);
    }

    public static void SetUseFloatingLabel(TemplatedControl element, bool value) {
        element.SetValue(UseFloatingLabelProperty, value);
    }
}