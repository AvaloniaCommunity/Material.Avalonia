using System;
using Avalonia.Controls;

namespace Material.Styles.Themes {
    public class ThemeChangedEventArgs : EventArgs {
        public ThemeChangedEventArgs(IResourceDictionary resourceDictionary, ITheme oldTheme, ITheme newTheme) {
            ResourceDictionary = resourceDictionary;
            OldTheme = oldTheme;
            NewTheme = newTheme;
        }

        public IResourceDictionary ResourceDictionary { get; }
        public ITheme NewTheme { get; }
        public ITheme OldTheme { get; }
    }
}