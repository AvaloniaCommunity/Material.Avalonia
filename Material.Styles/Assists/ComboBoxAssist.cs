using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists
{
    public static class ComboBoxAssist
    {
        public static readonly AvaloniaProperty<string?> LabelProperty =
            AvaloniaProperty.RegisterAttached<ComboBox, string?>(
                "Label", typeof(ComboBox));

        public static void SetLabel(AvaloniaObject element, string value) =>
            element.SetValue(LabelProperty, value);

        public static string? GetLabel(AvaloniaObject element) =>
            element.GetValue<string?>(LabelProperty);
    }
}