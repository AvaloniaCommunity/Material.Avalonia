using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ButtonAssist {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(4.0);

        #region AttachedProperty : CornerRadiusProperty

        /// <summary>
        ///     Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly AvaloniaProperty<CornerRadius> CornerRadiusProperty = AvaloniaProperty.RegisterAttached<Button, CornerRadius>(
            "CornerRadius", typeof(ButtonAssist), DefaultCornerRadius, true);

        public static CornerRadius GetCornerRadius(AvaloniaObject element) {
            return (CornerRadius) element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(AvaloniaObject element, CornerRadius value) {
            element.SetValue(CornerRadiusProperty, value);
        }
        
        
        public static AvaloniaProperty<SolidColorBrush> HoverColorProperty = AvaloniaProperty.RegisterAttached<Button, SolidColorBrush>(
            "HoverColor", typeof(ButtonAssist));
        
        public static void SetHoverColor(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(HoverColorProperty, value);
        }

        public static SolidColorBrush GetHoverColor(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(HoverColorProperty);
        }
        
        public static AvaloniaProperty<SolidColorBrush> ClickFeedbackColorProperty = AvaloniaProperty.RegisterAttached<Button, SolidColorBrush>(
            "ClickFeedbackColor", typeof(ButtonAssist));
        
        public static void SetClickFeedbackColor(AvaloniaObject element, SolidColorBrush value) {
            element.SetValue(ClickFeedbackColorProperty, value);
        }

        public static SolidColorBrush GetClickFeedbackColor(AvaloniaObject element) {
            return (SolidColorBrush) element.GetValue(ClickFeedbackColorProperty);
        }

        #endregion
    }
}