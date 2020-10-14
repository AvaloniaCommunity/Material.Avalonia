using Avalonia;
using Avalonia.Controls;

namespace Material.Styles {
    public class Card : ContentControl {
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<Border, CornerRadius>(nameof(CornerRadius), new CornerRadius(2));

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public CornerRadius CornerRadius {
            get { return GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}