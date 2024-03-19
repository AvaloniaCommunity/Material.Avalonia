using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;

namespace Material.Styles.Assists.Mixins;

/// <summary>
/// Adds BoxShadows transitions for any control, which has enabled animations.
/// </summary>
public static class MaterialBorderBoxShadowMixin {
    public static BoxShadowsTransition TargetTransition { get; set; } = new() { Property = Border.BoxShadowProperty, Duration = TimeSpan.FromMilliseconds(350) };

    /// <summary>
    /// Initializes BoxShadows transitions for specified control.
    /// </summary>
    /// <typeparam name="TControl">The control type.</typeparam>
    public static void Attach<TControl>() where TControl : Control {
        TransitionAssist.DisableTransitionsProperty.Changed.AddClassHandler<TControl>(OnDisableTransitionsChanged);
        Animatable.TransitionsProperty.Changed.AddClassHandler<TControl>(OnTransitionCollectionChanged);
    }

    private static void OnDisableTransitionsChanged<TControl>(TControl control, AvaloniaPropertyChangedEventArgs args)
        where TControl : Control {
        var transitions = control.GetValue(Animatable.TransitionsProperty);
        if (transitions is null) return;

        var disableTransitions = args.GetNewValue<bool>();
        ToggleTransitions(transitions, disableTransitions);
    }

    private static void OnTransitionCollectionChanged<TControl>(TControl control, AvaloniaPropertyChangedEventArgs args)
        where TControl : Control {
        var disableTransitions = (bool)control.GetValue(TransitionAssist.DisableTransitionsProperty)!;
        if (args.NewValue is Transitions transitions)
            ToggleTransitions(transitions, disableTransitions);
    }

    private static void ToggleTransitions(Transitions transitions, bool disableTransitions) {
        if (disableTransitions)
            transitions.Remove(TargetTransition);
        else if (!transitions.Contains(TargetTransition))
            transitions.Add(TargetTransition);
    }
}