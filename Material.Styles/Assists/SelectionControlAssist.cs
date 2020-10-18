using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class SelectionControlAssist {
        public static readonly AvaloniaProperty<double> SizeProperty
            = AvaloniaProperty.RegisterAttached<Button, double>("Size", typeof(SelectionControlAssist));

        public static double GetSize(Button element) {
            return (double) element.GetValue(SizeProperty);
        }

        public static void SetSize(Button element, double checkBoxSize) {
            element.SetValue(SizeProperty, checkBoxSize);
        }
    }
}