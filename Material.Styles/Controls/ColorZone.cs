using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Controls {
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
        
        public static readonly StyledProperty<ColorZoneMode> ModeProperty = AvaloniaProperty.Register<ColorZone, ColorZoneMode>(nameof(Mode));

        public ColorZoneMode Mode
        {
            get => GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }
    }
}