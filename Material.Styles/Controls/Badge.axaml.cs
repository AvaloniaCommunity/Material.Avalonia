using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Controls;

public class Badge : ContentControl {
    /// <summary>
    /// HasContent StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<bool> HasContentProperty =
        AvaloniaProperty.Register<Badge, bool>(nameof(HasContent));

    /// <summary>
    /// Gets or sets the HasContent property.
    /// </summary>
    public bool HasContent {
        get => GetValue(HasContentProperty);
        set => SetValue(HasContentProperty, value);
    }
}

