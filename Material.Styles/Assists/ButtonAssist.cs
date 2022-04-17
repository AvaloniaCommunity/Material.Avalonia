using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Material.Styles.Assists {
    public static class ButtonAssist {
        #region AttachedProperty : HoverColor

        public static AvaloniaProperty<IBrush> HoverColorProperty = 
            AvaloniaProperty.RegisterAttached<Button, IBrush>("HoverColor", typeof(ButtonAssist));
        
        public static void SetHoverColor(AvaloniaObject element, IBrush value) {
            element.SetValue(HoverColorProperty, value);
        }

        public static IBrush? GetHoverColor(AvaloniaObject element) {
            return element.GetValue(HoverColorProperty) as IBrush;
        }

        #endregion
        
        #region AttachedProperty : ClickFeedback

        public static AvaloniaProperty<IBrush> ClickFeedbackColorProperty = 
            AvaloniaProperty.RegisterAttached<Button, IBrush>("ClickFeedbackColor", typeof(ButtonAssist));
        
        public static void SetClickFeedbackColor(AvaloniaObject element, IBrush value) {
            element.SetValue(ClickFeedbackColorProperty, value);
        }

        public static IBrush? GetClickFeedbackColor(AvaloniaObject element) {
            return element.GetValue(ClickFeedbackColorProperty) as IBrush;
        }

        #endregion
    }
}