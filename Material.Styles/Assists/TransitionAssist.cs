using System;
using Avalonia;
using Avalonia.Data;

namespace Material.Styles.Assists {
    public static class TransitionAssist {
        /// <summary>
        ///     Allows transitions to be disabled where supported.  Note this is an inheritable property.
        /// </summary>
        public static readonly AvaloniaProperty<bool> DisableTransitionsProperty =
            AvaloniaProperty.RegisterAttached<AvaloniaObject, bool>(
                "DisableTransitions", typeof(TransitionAssist), false, true, BindingMode.TwoWay);

        static TransitionAssist() {
            DisableTransitionsProperty.Changed.Subscribe(args => {
                if (args.Sender is not StyledElement styledElement) return;
                styledElement.Classes.Set("no-transitions", args.NewValue.Value);

                // Compatibility with old Material.Styles.TransitionAssist
                // ReSharper disable once StringLiteralTypo
                styledElement.Classes.Set("notransitions", args.NewValue.Value);
            });
        }

        /// <summary>
        ///     Allows transitions to be disabled where supported.  Note this is an inheritable property.
        /// </summary>
        public static void SetDisableTransitions(AvaloniaObject element, bool value) {
            element.SetValue(DisableTransitionsProperty, value);
        }

        /// <summary>
        ///     Allows transitions to be disabled where supported.  Note this is an inheritable property.
        /// </summary>
        public static bool GetDisableTransitions(AvaloniaObject element) {
            return element.GetValue<bool>(DisableTransitionsProperty);
        }
    }
}
