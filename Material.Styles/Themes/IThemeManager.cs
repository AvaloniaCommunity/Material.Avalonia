using System;

namespace Material.Styles.Themes {
    [Obsolete($"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
    public interface IThemeManager {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }
}