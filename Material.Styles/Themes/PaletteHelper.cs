using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Themes {
    [Obsolete($"Obsolete styling system. Use {nameof(MaterialTheme)}. Details in our wiki: https://github.com/AvaloniaCommunity/Material.Avalonia/wiki/Advanced-Theming")]
    public class PaletteHelper {
        public virtual BundledTheme? LocateBundledTheme() {
            if (Application.Current == null)
                throw new InvalidOperationException("Cannot locate BundledTheme outside of a Avalonia application. Use ResourceDictionaryExtensions.GetTheme on the appropriate resource dictionary instead.");
            return LocateBundledThemeInternal(Application.Current.Resources);
        }

        private BundledTheme? LocateBundledThemeInternal(IResourceDictionary dictionary) {
            if (dictionary is BundledTheme bundledTheme) {
                return bundledTheme;
            }
            return dictionary.MergedDictionaries
                .Select(provider => provider as IResourceDictionary)
                .Where(resourceDictionary => resourceDictionary != null)
                .Select(LocateBundledThemeInternal!)
                .FirstOrDefault();
        }

        private IResourceDictionary GetBundledThemeOrRoot() {
            return LocateBundledTheme() ?? Application.Current.Resources;
        }

        public virtual ITheme GetTheme() {
            return GetBundledThemeOrRoot().GetTheme();
        }

        public virtual void SetTheme(ITheme theme) {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            GetBundledThemeOrRoot().SetTheme(theme);
        }

        public virtual IThemeManager GetThemeManager() {
            return GetBundledThemeOrRoot().GetThemeManager();
        }
    }
}