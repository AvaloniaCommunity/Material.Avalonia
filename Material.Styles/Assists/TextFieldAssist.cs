using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class TextFieldAssist
    {
        public static AvaloniaProperty<string> HintsProperty = AvaloniaProperty.RegisterAttached<TextBox, string>(
            "Hints", typeof(TextBox));
        
        public static void SetHints(AvaloniaObject element, string value) => element.SetValue(HintsProperty, value);

        public static string GetHints(AvaloniaObject element) => (string)element.GetValue(HintsProperty);

        public static AvaloniaProperty<string> LabelProperty = AvaloniaProperty.RegisterAttached<TextBox, string>(
            "Label", typeof(TextBox));

        public static void SetLabel(AvaloniaObject element, string value) => element.SetValue(LabelProperty, value);

        public static string GetLabel(AvaloniaObject element) => (string)element.GetValue(LabelProperty);
    }
}