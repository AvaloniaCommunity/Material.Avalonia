using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes;

public class CustomMaterialTheme : MaterialThemeBase, IDisposable {

    public static readonly StyledProperty<Color?> PrimaryColorProperty =
        AvaloniaProperty.Register<MaterialTheme, Color?>(nameof(PrimaryColor));

    public static readonly StyledProperty<Color?> SecondaryColorProperty =
        AvaloniaProperty.Register<MaterialTheme, Color?>(nameof(SecondaryColor));

    private readonly ITheme _theme = new Theme();

    private bool _isLoaded;
    private IThemeVariantHost? _lastThemeVariantHost;
    private IDisposable? _themeUpdateDisposable;
    private bool _disposedValue;

    public IDictionary<ThemeVariant, CustomMaterialThemeResources> Palettes { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialTheme"/> class.
    /// </summary>
    /// <param name="serviceProvider">The XAML service provider.</param>
    public CustomMaterialTheme(IServiceProvider serviceProvider) : base(serviceProvider) {
        var palettes = new AvaloniaDictionary<ThemeVariant, CustomMaterialThemeResources>(2);
        palettes.ForEachItem(
            (key, x) => {
                if (Owner is not null) {
                    ((IResourceProvider)x).AddOwner(Owner);
                }

                if (key != ThemeVariant.Dark && key != ThemeVariant.Light) {
                    throw new InvalidOperationException(
                        $"{nameof(CustomMaterialTheme)}.{nameof(CustomMaterialTheme.Palettes)} only supports Light and Dark variants.");
                }
            },
            (_, x) => {
                if (Owner is not null)
                    ((IResourceProvider)x).RemoveOwner(Owner);
            },
            () => throw new NotSupportedException("Dictionary reset not supported"));
        Palettes = palettes;

        OwnerChanged += OnOwnerChanged;
    }

    public Color? PrimaryColor {
        get => GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    public Color? SecondaryColor {
        get => GetValue(SecondaryColorProperty);
        set => SetValue(SecondaryColorProperty, value);
    }
    private void OnOwnerChanged(object? sender, EventArgs e) {
        RegisterActualThemeObservable();
    }

    protected override bool TryGetResource(object key, ThemeVariant? theme, out object? value) {
        return base.TryGetResource(key, theme, out value)
            || base.TryGetResource(key, GetVariantFromMaterialBaseThemeMode(ActualBaseTheme), out value);
    }

    private static ThemeVariant GetVariantFromMaterialBaseThemeMode(BaseThemeMode variant) {
        return variant switch {
            BaseThemeMode.Light => Theme.MaterialLight,
            BaseThemeMode.Dark => Theme.MaterialDark,
            _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
        };
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) {
        base.OnPropertyChanged(change);

        if (change.Property == BaseThemeProperty) {
            SetupActualTheme();
            return;
        }

        if (change.Property == ActualBaseThemeProperty) {
            var baseTheme = change.GetNewValue<BaseThemeMode>().GetBaseTheme();
            _theme.SetBaseTheme(baseTheme);
            if (GetPrimaryColor() is { } primaryColor) {
                _theme.SetPrimaryColor(primaryColor);
            }
            if (GetSecondaryColor() is { } secondaryColor) {
                _theme.SetSecondaryColor(secondaryColor);
            }
            EnqueueThemeUpdate();
            return;
        }

        if (change.Property == PrimaryColorProperty) {
            if (GetPrimaryColor() is { } primaryColor) {
                _theme.SetPrimaryColor(primaryColor);
                EnqueueThemeUpdate();
            }
            return;
        }

        if (change.Property == SecondaryColorProperty) {
            if (GetSecondaryColor() is { } secondaryColor) {
                _theme.SetSecondaryColor(secondaryColor);
                EnqueueThemeUpdate();
            }
        }
    }

    private Color? GetPrimaryColor() {
        Color? color = null;
        if (GetActualThemeVariant() is { } themeVariant &&
            Palettes.TryGetValue(themeVariant, out var colorPalate)) {
            color = colorPalate.PrimaryColor;
        }
        return color ?? PrimaryColor;
    }

    private Color? GetSecondaryColor() {
        Color? color = null;
        if (GetActualThemeVariant() is { } themeVariant &&
            Palettes.TryGetValue(themeVariant, out var colorPalate)) {
            color = colorPalate.SecondaryColor;
        }
        return color ?? SecondaryColor;
    }

    private void EnqueueThemeUpdate() {
        if (!_isLoaded)
            return;

        _themeUpdateDisposable?.Dispose();
        _themeUpdateDisposable = DispatcherTimer.RunOnce(() => CurrentTheme = _theme, TimeSpan.FromMilliseconds(100));
    }

    private void RegisterActualThemeObservable() {
        if (_lastThemeVariantHost is not null)
            _lastThemeVariantHost.ActualThemeVariantChanged -= HostOnActualThemeVariantChanged;

        _lastThemeVariantHost = Owner as IThemeVariantHost;
        if (_lastThemeVariantHost is not null)
            _lastThemeVariantHost.ActualThemeVariantChanged += HostOnActualThemeVariantChanged;
        SetupActualTheme();
    }

    private void HostOnActualThemeVariantChanged(object? sender, EventArgs e) {
        SetupActualTheme();
    }

    private void SetupActualTheme() {
        var materialBaseThemeModeFromVariant = BaseTheme switch {
            BaseThemeMode.Inherit => GetMaterialBaseThemeModeFromVariant(_lastThemeVariantHost?.ActualThemeVariant) ?? BaseThemeMode.Light,
            BaseThemeMode.Light => BaseThemeMode.Light,
            BaseThemeMode.Dark => BaseThemeMode.Dark,
            _ => throw new ArgumentOutOfRangeException(nameof(BaseTheme), BaseTheme, null)
        };

        ActualBaseTheme = materialBaseThemeModeFromVariant;
    }

    private static BaseThemeMode? GetMaterialBaseThemeModeFromVariant(ThemeVariant? variant) {
        while (true) {
            if (variant is null)
                return null;
            if (variant == ThemeVariant.Light)
                return BaseThemeMode.Light;
            if (variant == ThemeVariant.Dark)
                return BaseThemeMode.Dark;
            variant = variant.InheritVariant;
        }
    }

    private ThemeVariant? GetActualThemeVariant() {
        return ActualBaseTheme switch {
            BaseThemeMode.Light => ThemeVariant.Light,
            BaseThemeMode.Dark => ThemeVariant.Dark,
            _ => null
        };
    }

    protected override ITheme ProvideInitialTheme() {
        _isLoaded = true;
        return _theme;
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposedValue) {
            if (disposing) {
                _themeUpdateDisposable?.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}