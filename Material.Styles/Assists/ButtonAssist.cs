using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Material.Styles.Internal;

namespace Material.Styles.Assists
{
    public static class ButtonAssist
    {
        #region AttachedProperty : HoverColor

        public static readonly AvaloniaProperty<IBrush?> HoverColorProperty =
            AvaloniaProperty.RegisterAttached<Button, IBrush?>("HoverColor", typeof(ButtonAssist));

        public static void SetHoverColor(AvaloniaObject element, IBrush? value) =>
            element.SetValue(HoverColorProperty, value);

        public static IBrush? GetHoverColor(AvaloniaObject element) =>
            element.GetValue<IBrush?>(HoverColorProperty);

        #endregion

        #region AttachedProperty : ClickFeedback

        public static readonly AvaloniaProperty<IBrush?> ClickFeedbackColorProperty =
            AvaloniaProperty.RegisterAttached<Button, IBrush?>("ClickFeedbackColor", typeof(ButtonAssist));

        public static void SetClickFeedbackColor(AvaloniaObject element, IBrush value) =>
            element.SetValue(ClickFeedbackColorProperty, value);

        public static IBrush? GetClickFeedbackColor(AvaloniaObject element) =>
            element.GetValue<IBrush?>(ClickFeedbackColorProperty);

        #endregion
        
        // Used for memorise which hyperlink button clicked (or just say, visited)
        #region AttachedProperty : IsClicked

        public static readonly AvaloniaProperty<bool?> IsClickedProperty =
            AvaloniaProperty.RegisterAttached<Button, bool?>("IsClicked", typeof(ButtonAssist));

        public static void SetIsClicked(AvaloniaObject element, bool? value) =>
            element.SetValue(IsClickedProperty, value);

        public static bool? GetIsClicked(AvaloniaObject element) =>
            element.GetValue<bool?>(IsClickedProperty);

        
        private static void OnButtonClickedPrivate((object, RoutedEventArgs) args) {
            if(args.Item1 is not Button button)
                return;

            var value = button.GetValue<bool?>(IsClickedProperty);
            
            // null means not required for handling
            if(!value.HasValue)
                return;

            // if IsClickedProperty is false, put it to true
            if (!value.Value)
                button.SetValue(IsClickedProperty, true);
            
            if (button is not HyperlinkButton hyperlink)
                return;

            hyperlink.IsVisited = true;
        }

        #endregion

        #region Initiator (used for observe control changes)

        static ButtonAssist() {
            Button.ClickEvent.Raised.Subscribe(OnButtonClickedPrivate);
        }

        #endregion
    }
}