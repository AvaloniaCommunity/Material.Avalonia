using Avalonia;
using Avalonia.Controls;

namespace Material.Styles {
    public class Card : ContentControl {
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<Card, CornerRadius>(nameof(CornerRadius), new CornerRadius(4));

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public CornerRadius CornerRadius {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly StyledProperty<bool> InsideClippingProperty =
AvaloniaProperty.Register<Card, bool>(nameof(InsideClipping), true);

        /// <summary>
        /// Get or set the inside border clipping.
        /// </summary>
        public bool InsideClipping
        {
            get { return GetValue(InsideClippingProperty); }
            set { SetValue(InsideClippingProperty, value); }
        }
    }
}