using System;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Material.Colors.ColorManipulation;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes {
    public static class ThemeExtensions {
        public static T LocateMaterialTheme<T>(this Application application) where T : MaterialThemeBase {
            var materialTheme = application.Styles.FirstOrDefault(style => style is T);
            if (materialTheme == null) {
                throw new InvalidOperationException(
                    $"Cannot locate {nameof(T)} in Avalonia application styles. Be sure that you include MaterialTheme in your App.xaml in Application.Styles section");
            }

            return (T)materialTheme;
        }

        public static IBaseTheme GetBaseTheme(this BaseThemeMode baseThemeMode) {
            return baseThemeMode switch {
                BaseThemeMode.Dark    => Theme.Dark,
                BaseThemeMode.Light   => Theme.Light,
                BaseThemeMode.Inherit => Theme.Light,
                _                     => throw new InvalidOperationException()
            };
        }

        [Obsolete("Use GetBaseThemeMode")]
        public static BaseThemeMode GetBaseTheme(this IReadOnlyTheme theme)
            => GetBaseThemeMode(theme);

        public static BaseThemeMode GetBaseThemeMode(this IReadOnlyTheme theme) {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            var foreground = theme.Background.PickContrastColor();
            return foreground == Avalonia.Media.Colors.Black ? BaseThemeMode.Light : BaseThemeMode.Dark;
        }

        /// <summary>
        /// Invert the <see cref="BaseThemeMode"/>.
        /// </summary>
        /// <param name="mode">Initial mode to invert</param>
        /// <returns>
        /// <see cref="BaseThemeMode.Dark"/> for <see cref="BaseThemeMode.Light"/> <br/>
        /// <see cref="BaseThemeMode.Light"/> for <see cref="BaseThemeMode.Dark"/> <br/>
        /// Everything else remains unchanged
        /// </returns>
        public static BaseThemeMode Invert(this BaseThemeMode mode) {
            return mode switch {
                BaseThemeMode.Light => BaseThemeMode.Dark,
                BaseThemeMode.Dark  => BaseThemeMode.Light,
                _                   => mode
            };
        }

        public static ITheme SetBaseTheme(this ITheme theme, IBaseTheme baseTheme) {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.ValidationError = baseTheme.MaterialValidationErrorColor;
            theme.Background = baseTheme.MaterialBackgroundColor;
            theme.Paper = baseTheme.MaterialPaperColor;
            theme.CardBackground = baseTheme.MaterialCardBackgroundColor;
            theme.ToolBarBackground = baseTheme.MaterialToolBarBackgroundColor;
            theme.Body = baseTheme.MaterialBodyColor;
            theme.BodyLight = baseTheme.MaterialBodyLightColor;
            theme.ColumnHeader = baseTheme.MaterialColumnHeaderColor;
            theme.CheckBoxOff = baseTheme.MaterialCheckBoxOffColor;
            theme.CheckBoxDisabled = baseTheme.MaterialCheckBoxDisabledColor;
            theme.Divider = baseTheme.MaterialDividerColor;
            theme.Selection = baseTheme.MaterialSelectionColor;
            theme.ToolForeground = baseTheme.MaterialToolForegroundColor;
            theme.ToolBackground = baseTheme.MaterialToolBackgroundColor;
            theme.FlatButtonClick = baseTheme.MaterialFlatButtonClickColor;
            theme.FlatButtonRipple = baseTheme.MaterialFlatButtonRippleColor;
            theme.ToolTipBackground = baseTheme.MaterialToolTipBackgroundColor;
            theme.ChipBackground = baseTheme.MaterialChipBackgroundColor;
            theme.SnackbarBackground = baseTheme.MaterialSnackbarBackgroundColor;
            theme.SnackbarMouseOver = baseTheme.MaterialSnackbarMouseOverColor;
            theme.SnackbarRipple = baseTheme.MaterialSnackbarRippleColor;
            theme.TextBoxBorder = baseTheme.MaterialTextBoxBorderColor;
            theme.TextFieldBoxBackground = baseTheme.MaterialTextFieldBoxBackgroundColor;
            theme.TextFieldBoxHoverBackground = baseTheme.MaterialTextFieldBoxHoverBackgroundColor;
            theme.TextFieldBoxDisabledBackground = baseTheme.MaterialTextFieldBoxDisabledBackgroundColor;
            theme.TextAreaBorder = baseTheme.MaterialTextAreaBorderColor;
            theme.TextAreaInactiveBorder = baseTheme.MaterialTextAreaInactiveBorderColor;
            theme.DataGridRowHoverBackground = baseTheme.MaterialDataGridRowHoverBackgroundColor;

            return theme;
        }

        public static ITheme SetPrimaryColor(this ITheme theme, Color primaryColor) {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.PrimaryLight = primaryColor.Lighten();
            theme.PrimaryMid = primaryColor;
            theme.PrimaryDark = primaryColor.Darken();

            return theme;
        }

        public static ITheme SetSecondaryColor(this ITheme theme, Color accentColor) {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            theme.SecondaryLight = accentColor.Lighten();
            theme.SecondaryMid = accentColor;
            theme.SecondaryDark = accentColor.Darken();

            return theme;
        }

        /// <summary>
        /// Create a shallow copy of <see cref="IReadOnlyTheme"/> and return mutable <see cref="ITheme"/> 
        /// </summary>
        /// <param name="readOnlyTheme">Initial read only theme</param>
        /// <returns>Mutable copy of read only theme</returns>
        public static ITheme ToMutable(this IReadOnlyTheme readOnlyTheme)
            => Theme.Create(readOnlyTheme);
    }
}
