using Avalonia.Controls;
using MaterialColors;
using MaterialXamlToolKit.Avalonia.Themes.Base;

namespace MaterialXamlToolKit.Avalonia.Themes
{
    public class BundledTheme : ResourceDictionary
    {
        private BaseThemeMode? _baseTheme;
        public BaseThemeMode? BaseTheme
        {
            get { return _baseTheme; }
            set
            {
                if (_baseTheme != value)
                {
                    _baseTheme = value;
                    SetTheme();
                }
            }
        }

        private PrimaryColor? _primaryColor;
        public PrimaryColor? PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    SetTheme();
                }
            }
        }

        private SecondaryColor? _secondaryColor;
        public SecondaryColor? SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                if (_secondaryColor != value)
                {
                    _secondaryColor = value;
                    SetTheme();
                }
            }
        }

        private void SetTheme()
        {
            if (BaseTheme is BaseThemeMode baseTheme &&
                PrimaryColor is PrimaryColor primaryColor &&
                SecondaryColor is SecondaryColor secondaryColor)
            {
                var theme = Theme.Create(baseTheme.GetBaseTheme(),
                    SwatchHelper.Lookup[(MaterialColor)primaryColor],
                    SwatchHelper.Lookup[(MaterialColor)secondaryColor]);

                ApplyTheme(theme);
            }
        }

        protected virtual void ApplyTheme(ITheme theme)
        {
            this.SetTheme(theme);
        }
    }
}