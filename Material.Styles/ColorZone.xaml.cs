using Avalonia;
using Avalonia.Controls;

namespace Material.Styles {
    public enum ColorZoneMode
    {
        Standard,
        Inverted,
        PrimaryLight,
        PrimaryMid,
        PrimaryDark,
        Accent,
        Light,
        Dark,
        Custom
    }
    public class ColorZone : ContentControl {
        public static readonly StyledProperty<CornerRadius> CornerRadiusProperty =
            AvaloniaProperty.Register<ColorZone, CornerRadius>(nameof(CornerRadius));

        /// <summary>
        /// Gets or sets the radius of the border rounded corners.
        /// </summary>
        public CornerRadius CornerRadius {
            get => GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        public static readonly StyledProperty<ColorZoneMode> ModeProperty = AvaloniaProperty.Register<ColorZone, ColorZoneMode>(nameof(Mode));

        public ColorZoneMode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}