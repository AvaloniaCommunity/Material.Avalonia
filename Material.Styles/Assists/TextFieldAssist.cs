using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

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

        public static AvaloniaProperty<bool> HasErrorProperty = AvaloniaProperty.RegisterAttached<TextBox, bool>(
            "HasError", typeof(TextBox));

        public static void SetHasError(AvaloniaObject element, bool value) => element.SetValue(HasErrorProperty, value);

        public static bool GetHasError(AvaloniaObject element) => (bool)element.GetValue(HasErrorProperty);


        public static AvaloniaProperty<SolidColorBrush> ErrorColorProperty = 
            AvaloniaProperty.RegisterAttached<TextBox, SolidColorBrush>("ErrorColor", typeof(TextFieldAssist), SolidColorBrush.Parse("#f44336"));

        public static void SetErrorColor(AvaloniaObject element, SolidColorBrush value)
        {
            element.SetValue(ErrorColorProperty, value);
        }

        public static SolidColorBrush GetErrorColor(AvaloniaObject element)
        {
            return (SolidColorBrush)element.GetValue(ErrorColorProperty);
        }
        
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(0);
        
        /// <summary>
        ///     Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly AvaloniaProperty<CornerRadius> CornerRadiusProperty = AvaloniaProperty.RegisterAttached<TextBox, CornerRadius>(
            "CornerRadius", typeof(TextFieldAssist), DefaultCornerRadius, true);

        public static CornerRadius GetCornerRadius(AvaloniaObject element) {
            return (CornerRadius) element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(AvaloniaObject element, CornerRadius value) {
            element.SetValue(CornerRadiusProperty, value);
        }
    }
}