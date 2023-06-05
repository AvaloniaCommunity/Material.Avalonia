using System;
using Avalonia.Controls;
using Avalonia.Threading;
using Material.Colors;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes;

[Obsolete($"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
public class BundledTheme : ResourceDictionary {
    private BaseThemeMode? _baseTheme;

    private PrimaryColor? _primaryColor;

    private SecondaryColor? _secondaryColor;

    public BaseThemeMode? BaseTheme {
        get => _baseTheme;
        set {
            if (_baseTheme != value) {
                _baseTheme = value;
                SetTheme();
            }
        }
    }

    public PrimaryColor? PrimaryColor {
        get => _primaryColor;
        set {
            if (_primaryColor != value) {
                _primaryColor = value;
                SetTheme();
            }
        }
    }

    public SecondaryColor? SecondaryColor {
        get => _secondaryColor;
        set {
            if (_secondaryColor != value) {
                _secondaryColor = value;
                SetTheme();
            }
        }
    }

    private void SetTheme() {
        if (!(BaseTheme is { } baseTheme) || !(PrimaryColor is { } primaryColor) ||
            !(SecondaryColor is { } secondaryColor)) return;
        var theme = Theme.Create(baseTheme.GetBaseTheme(),
            SwatchHelper.Lookup[(MaterialColor)primaryColor],
            SwatchHelper.Lookup[(MaterialColor)secondaryColor]);

        ApplyTheme(theme);
    }

    protected virtual void ApplyTheme(ITheme theme) {
        this.SetTheme(theme);

        // Workaround for TextBlock text color does not changing: https://github.com/AvaloniaUI/Avalonia/issues/9675
        // Actually, do not use BundledTheme, use MaterialTheme instead
        Dispatcher.UIThread.InvokeAsync(() => {
            var bodyBrush = this["MaterialDesignBody"];
            Remove("MaterialDesignBody");
            Add("MaterialDesignBody", bodyBrush);
        }, DispatcherPriority.Normal);
    }
}

