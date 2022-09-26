using System;
using Avalonia.Controls;

namespace Material.Styles.Themes
{
    [Obsolete(
        $"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(IResourceDictionary resourceDictionary, ITheme? oldTheme, ITheme newTheme)
        {
            ResourceDictionary = resourceDictionary;
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }

        public IResourceDictionary ResourceDictionary { get; }
        public ITheme NewTheme { get; }
        public ITheme? OldTheme { get; }
    }
}