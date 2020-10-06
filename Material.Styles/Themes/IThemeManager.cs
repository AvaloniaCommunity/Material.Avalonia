using System;

namespace Material.Styles.Themes {
    public interface IThemeManager {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }
}