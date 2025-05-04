using System;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Material.Ripple;
using Material.Styles.Assists;

namespace Material.Avalonia.Demo.Assists;

public static class AutomationAssist {
    #region Initiator (used for observe control changes)
    
    // This placeholder is used for anti-optimising to keep it persisted, also triggering static constructor
    internal static void Placeholder() {
        Console.WriteLine("Automation assist is initiate...");
    }

    static AutomationAssist() {
        Button.ClickEvent.Raised
            .Subscribe(new AnonymousObserver<(object, RoutedEventArgs)>(OnButtonClickedPrivate));
    }

    #endregion

    // Used for memorise which hyperlink button clicked (or just say, visited)
    // this property can be attached to any buttons for use
    // we have confirmed that this property doesn't need to integrate to the theming library
    // but it can be used as an example
    #region AttachedProperty : IsClicked

    public static readonly AvaloniaProperty<bool?> IsClickedProperty =
        AvaloniaProperty.RegisterAttached<Button, bool?>("IsClicked", typeof(ButtonAssist));

    public static void SetIsClicked(AvaloniaObject element, bool? value) =>
        element.SetValue(IsClickedProperty, value);

    public static bool? GetIsClicked(AvaloniaObject element) =>
        element.GetValue<bool?>(IsClickedProperty);


    private static void OnButtonClickedPrivate((object, RoutedEventArgs) args) {
        if (args.Item1 is not Button button)
            return;

        UpdateIsClickedPropertyPrivate(button);
        RaiseRipplePrivate(button);
    }

    internal static void RaiseRipplePrivate(Control c) {
        // Try to find first RippleEffect control with name PART_Ripple
        var visual = c
            .GetVisualDescendants()
            .FirstOrDefault(a => a is RippleEffect && a.Name == "PART_Ripple");

        // if such control not exist or mouse is over on it 
        if (visual is not RippleEffect effect || effect.IsPointerOver)
            return;

        effect.RaiseRipple();
    }

    private static void UpdateIsClickedPropertyPrivate(Button button) {
        var value = GetIsClicked(button);

        // null means not required for handling
        if (!value.HasValue && button is not HyperlinkButton)
            return;

        value = false;
        
        // if IsClickedProperty is false, put it to true
        if (!value.Value)
            SetIsClicked(button, true);

        if (button is not HyperlinkButton hyperlink)
            return;

        // change Hyperlink button is visited state
        hyperlink.IsVisited = true;
    }

    #endregion
}