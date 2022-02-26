using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Material.Styles {
    public class Card : ContentControl {
        public static readonly StyledProperty<bool> InsideClippingProperty = 
            AvaloniaProperty.Register<Card, bool>(nameof(InsideClipping), true);

        /// <summary>
        /// Get or set the inside border clipping.
        /// </summary>
        public bool InsideClipping
        {
            get => GetValue(InsideClippingProperty);
            set => SetValue(InsideClippingProperty, value);
        }
    }
}