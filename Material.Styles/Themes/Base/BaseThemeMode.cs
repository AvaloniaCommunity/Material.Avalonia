using System;
using Avalonia.Styling;

namespace Material.Styles.Themes.Base; 

public enum BaseThemeMode {
    Inherit,
    Light,
    Dark
}
internal static class BaseThemeModeExtensions {
    public static BaseThemeMode? GetMaterialBaseThemeModeFromVariant(this ThemeVariant? variant) {
        while (true) {
            if (variant is null) return null;
            if (variant == ThemeVariant.Light) return BaseThemeMode.Light;
            if (variant == ThemeVariant.Dark) return BaseThemeMode.Dark;
            variant = variant.InheritVariant;
        }
    }

    public static ThemeVariant GetVariantFromMaterialBaseThemeMode(this BaseThemeMode variant) {
        return variant switch {
            BaseThemeMode.Light => Theme.MaterialLight,
            BaseThemeMode.Dark  => Theme.MaterialDark,
            _                   => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
        };
    }
}