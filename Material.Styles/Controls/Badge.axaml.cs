using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;

namespace Material.Styles.Controls;

[PseudoClasses(":display-content")]
public class Badge : ContentControl {
    /// <summary>
    /// Identifies the <see cref="ColorMode"/> property.
    /// </summary>
    public static readonly StyledProperty<ColorZoneMode> ColorModeProperty = 
        AvaloniaProperty.Register<Badge, ColorZoneMode>(nameof(ColorMode), ColorZoneMode.Error);

    /// <summary>
    /// Gets or sets the color zone mode
    /// </summary>
    public ColorZoneMode ColorMode {
        get => GetValue(ColorModeProperty);
        set => SetValue(ColorModeProperty, value);
    }

    /// <summary>
    /// Identifies the <see cref="DisplayContent"/> property.
    /// </summary>
    public static readonly StyledProperty<bool?> DisplayContentProperty =
        AvaloniaProperty.Register<Badge, bool?>(nameof(DisplayContent));

    /// <summary>
    /// Gets or sets whether the badge content is displayed.
    /// </summary>
    public bool? DisplayContent {
        get => GetValue(DisplayContentProperty);
        set => SetValue(DisplayContentProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) {
        base.OnPropertyChanged(change);
        if (change.Property == DisplayContentProperty || change.Property == ContentProperty) {
            UpdatePseudoClasses();
        }
    }

    private void UpdatePseudoClasses() {
        var displayContent = (DisplayContent, Content) switch {
            (true, _) => true,
            (null, 0) => false,
            (null, "0") => false,
            (null, "") => false,
            (null, not null) => true,
            (_, _) => false,
        };
        PseudoClasses.Set(":content-hidden", !displayContent);
    }
}