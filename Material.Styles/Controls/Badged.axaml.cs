using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Material.Styles.Enums;

namespace Material.Styles.Controls;
public class Badged : ContentControl {
    /// <summary>
    /// BadgeContent StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<object?> BadgeContentProperty =
        AvaloniaProperty.Register<Badged, object?>(nameof(BadgeContent));
    /// <summary>
    /// Gets or sets the BadgeContent property.
    /// </summary>
    public object? BadgeContent {
        get => GetValue(BadgeContentProperty);
        set => SetValue(BadgeContentProperty, value);
    }

    /// <summary>
    /// BadgeBackground StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<Brush> BadgeBackgroundProperty =
        AvaloniaProperty.Register<Badged, Brush>(nameof(BadgeBackground));
    /// <summary>
    /// Gets or sets the BadgeBackground property.
    /// </summary>
    public Brush BadgeBackground {
        get => GetValue(BadgeBackgroundProperty);
        set => SetValue(BadgeBackgroundProperty, value);
    }

    /// <summary>
    /// BadgeForeground StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<Brush> BadgeForegroundProperty =
        AvaloniaProperty.Register<Badged, Brush>(nameof(BadgeForeground));
    /// <summary>
    /// Gets or sets the BadgeForeground property.
    /// </summary>
    public Brush BadgeForeground {
        get => GetValue(BadgeForegroundProperty);
        set => SetValue(BadgeForegroundProperty, value);
    }

    /// <summary>
    /// BadgeCornerRadius StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<CornerRadius> BadgeCornerRadiusProperty =
        AvaloniaProperty.Register<Badged, CornerRadius>(nameof(BadgeCornerRadius));
    /// <summary>
    /// Gets or sets the BadgeCornerRadius property.
    /// </summary>
    public CornerRadius BadgeCornerRadius {
        get => GetValue(BadgeCornerRadiusProperty);
        set => SetValue(BadgeCornerRadiusProperty, value);
    }

    /// <summary>
    /// BadgePlacement StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<BadgePlacement> BadgePlacementProperty =
        AvaloniaProperty.Register<Badged, BadgePlacement>(nameof(BadgePlacement));
    /// <summary>
    /// Gets or sets the BadgePlacement property.
    /// </summary>
    public BadgePlacement BadgePlacement {
        get => GetValue(BadgePlacementProperty);
        set => SetValue(BadgePlacementProperty, value);
    }

    /// <summary>
    /// BadgeHasContent StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<bool?> BadgeDisplayContentProperty =
        AvaloniaProperty.Register<Badged, bool?>(nameof(BadgeDisplayContent));
    /// <summary>
    /// Gets or sets the BadgeHasContent property.
    /// </summary>
    public bool? BadgeDisplayContent {
        get => GetValue(BadgeDisplayContentProperty);
        set => SetValue(BadgeDisplayContentProperty, value);
    }

    /// <summary>
    /// IsBadgeVisible StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<bool> IsBadgeVisibleProperty =
        AvaloniaProperty.Register<Badged, bool>(nameof(IsBadgeVisible));
    /// <summary>
    /// Gets or sets the IsBadgeVisible property.
    /// </summary>
    public bool IsBadgeVisible {
        get => GetValue(IsBadgeVisibleProperty);
        set => SetValue(IsBadgeVisibleProperty, value);
    }

    /// <summary>
    /// BadgeWidth StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<double> BadgeWidthProperty =
        AvaloniaProperty.Register<Badged, double>(nameof(BadgeWidth), double.NaN);
    /// <summary>
    /// Gets or sets the BadgeWidth property.
    /// </summary>
    public double BadgeWidth {
        get => GetValue(BadgeWidthProperty);
        set => SetValue(BadgeWidthProperty, value);
    }

    /// <summary>
    /// BadgeHeight StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<double> BadgeHeightProperty =
        AvaloniaProperty.Register<Badged, double>(nameof(BadgeHeight), double.NaN);
    /// <summary>
    /// Gets or sets the BadgeHeight property.
    /// </summary>
    public double BadgeHeight {
        get => GetValue(BadgeHeightProperty);
        set => SetValue(BadgeHeightProperty, value);
    }

    /// <summary>
    /// BadgeFontSize StyledProperty definition.
    /// </summary>
    public static readonly StyledProperty<double> BadgeFontSizeProperty =
        AvaloniaProperty.Register<Badged, double>(nameof(BadgeFontSize));
    /// <summary>
    /// Gets or sets the BadgeFontSize property.
    /// </summary>
    public double BadgeFontSize {
        get => GetValue(BadgeFontSizeProperty);
        set => SetValue(BadgeFontSizeProperty, value);
    }
}
