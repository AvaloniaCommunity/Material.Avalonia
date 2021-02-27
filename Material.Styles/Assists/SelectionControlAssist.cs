using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class SelectionControlAssist {
        #region Size of selection widget

        public static readonly AvaloniaProperty<double> SizeProperty
            = AvaloniaProperty.RegisterAttached<Button, double>("Size", typeof(SelectionControlAssist));

        public static double GetSize(Button element) {
            return (double) element.GetValue(SizeProperty);
        }

        public static void SetSize(Button element, double checkBoxSize) {
            element.SetValue(SizeProperty, checkBoxSize);
        }

        #endregion

        #region Main selection widget foreground

        public static readonly AvaloniaProperty<Brush> ForegroundProperty
            = AvaloniaProperty.RegisterAttached<Button, Brush>("Foreground", typeof(SelectionControlAssist));

        public static Brush GetForeground(Button element) {
            return (Brush) element.GetValue(ForegroundProperty);
        }

        public static void SetForeground(Button element, Brush brush) {
            element.SetValue(ForegroundProperty, brush);
        }

        #endregion

        #region Inner widget foreground color of selection widget

        public static readonly AvaloniaProperty<Brush> InnerForegroundProperty
            = AvaloniaProperty.RegisterAttached<Button, Brush>("InnerForeground", typeof(SelectionControlAssist));

        public static Brush GetInnerForeground(Button element) {
            return (Brush) element.GetValue(InnerForegroundProperty);
        }

        public static void SetInnerForeground(Button element, Brush brush) {
            element.SetValue(InnerForegroundProperty, brush);
        }

        #endregion
    }
}