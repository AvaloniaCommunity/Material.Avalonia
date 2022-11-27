using System;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Material.Colors.ColorManipulation;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes
{
    public static class ThemeExtensions
    {
        public static T LocateMaterialTheme<T>(this Application application) where T : MaterialThemeBase
        {
            var materialTheme = application.Styles.FirstOrDefault(style => style is T);
            if (materialTheme == null)
            {
                throw new InvalidOperationException(
                    $"Cannot locate {nameof(T)} in Avalonia application styles. Be sure that you include MaterialTheme in your App.xaml in Application.Styles section");
            }

            return (T) materialTheme;
        }

        public static IBaseTheme GetBaseTheme(this BaseThemeMode baseThemeMode)
        {
            return baseThemeMode switch
            {
                BaseThemeMode.Dark    => Theme.Dark,
                BaseThemeMode.Light   => Theme.Light,
                BaseThemeMode.Inherit => (SystemThemeProbe.GetSystemBaseThemeMode() ?? BaseThemeMode.Light).GetBaseTheme(),
                _                     => throw new InvalidOperationException()
            };
        }

        public static BaseThemeMode GetBaseTheme(this ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            var foreground = theme.Background.PickContrastColor();
            return foreground == Avalonia.Media.Colors.Black ? BaseThemeMode.Light : BaseThemeMode.Dark;
        }

        public static ITheme SetBaseTheme(this ITheme theme, IBaseTheme baseTheme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.ValidationError = baseTheme.ValidationErrorColor;
            theme.Background = baseTheme.MaterialDesignBackground;
            theme.Paper = baseTheme.MaterialDesignPaper;
            theme.CardBackground = baseTheme.MaterialDesignCardBackground;
            theme.ToolBarBackground = baseTheme.MaterialDesignToolBarBackground;
            theme.Body = baseTheme.MaterialDesignBody;
            theme.BodyLight = baseTheme.MaterialDesignBodyLight;
            theme.ColumnHeader = baseTheme.MaterialDesignColumnHeader;
            theme.CheckBoxOff = baseTheme.MaterialDesignCheckBoxOff;
            theme.CheckBoxDisabled = baseTheme.MaterialDesignCheckBoxDisabled;
            theme.Divider = baseTheme.MaterialDesignDivider;
            theme.Selection = baseTheme.MaterialDesignSelection;
            theme.ToolForeground = baseTheme.MaterialDesignToolForeground;
            theme.ToolBackground = baseTheme.MaterialDesignToolBackground;
            theme.FlatButtonClick = baseTheme.MaterialDesignFlatButtonClick;
            theme.FlatButtonRipple = baseTheme.MaterialDesignFlatButtonRipple;
            theme.ToolTipBackground = baseTheme.MaterialDesignToolTipBackground;
            theme.ChipBackground = baseTheme.MaterialDesignChipBackground;
            theme.SnackbarBackground = baseTheme.MaterialDesignSnackbarBackground;
            theme.SnackbarMouseOver = baseTheme.MaterialDesignSnackbarMouseOver;
            theme.SnackbarRipple = baseTheme.MaterialDesignSnackbarRipple;
            theme.TextBoxBorder = baseTheme.MaterialDesignTextBoxBorder;
            theme.TextFieldBoxBackground = baseTheme.MaterialDesignTextFieldBoxBackground;
            theme.TextFieldBoxHoverBackground = baseTheme.MaterialDesignTextFieldBoxHoverBackground;
            theme.TextFieldBoxDisabledBackground = baseTheme.MaterialDesignTextFieldBoxDisabledBackground;
            theme.TextAreaBorder = baseTheme.MaterialDesignTextAreaBorder;
            theme.TextAreaInactiveBorder = baseTheme.MaterialDesignTextAreaInactiveBorder;
            theme.DataGridRowHoverBackground = baseTheme.MaterialDesignDataGridRowHoverBackground;

            return theme;
        }

        public static ITheme SetPrimaryColor(this ITheme theme, Color primaryColor)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.PrimaryLight = primaryColor.Lighten();
            theme.PrimaryMid = primaryColor;
            theme.PrimaryDark = primaryColor.Darken();

            return theme;
        }

        public static ITheme SetSecondaryColor(this ITheme theme, Color accentColor)
        {
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