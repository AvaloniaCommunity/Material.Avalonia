using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists
{
    public static class TextFieldAssist
    {
        public static readonly AvaloniaProperty<string?> HintsProperty =
            AvaloniaProperty.RegisterAttached<TextBox, string?>(
                "Hints", typeof(TextBox));

        public static void SetHints(AvaloniaObject element, string? value) =>
            element.SetValue(HintsProperty, value);

        public static string? GetHints(AvaloniaObject element) =>
            element.GetValue<string?>(HintsProperty);

        public static readonly AvaloniaProperty<string?> LabelProperty =
            AvaloniaProperty.RegisterAttached<TextBox, string?>(
                "Label", typeof(TextBox));

        public static void SetLabel(AvaloniaObject element, string? value) =>
            element.SetValue(LabelProperty, value);

        public static string? GetLabel(AvaloniaObject element) =>
            element.GetValue<string?>(LabelProperty);

        // Use throw DataValidationException in property of view model instead of those things
        // Example can be found in dev-branch -> /Material.Demo/ViewModels/TextFieldsViewModel.cs

        /*
        
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
        
        */
    }
}