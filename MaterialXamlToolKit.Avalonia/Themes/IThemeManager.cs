using System;

namespace MaterialXamlToolKit.Avalonia.Themes
{
    public interface IThemeManager
    {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
    }
}