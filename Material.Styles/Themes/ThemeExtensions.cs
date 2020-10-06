using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Media;
using MaterialColors;
using MaterialColors.ColorManipulation;
using MaterialXamlToolKit.Avalonia.Themes.Base;

namespace MaterialXamlToolKit.Avalonia.Themes
{
    public static class ThemeExtensions
    {
        internal static ColorPair ToPairedColor(this Hue hue) 
            => new ColorPair(hue.Color, hue.Foreground);
        
        public static IBaseTheme GetBaseTheme(this BaseThemeMode baseThemeMode) {
            return baseThemeMode switch {
                BaseThemeMode.Dark  => Theme.Dark,
                BaseThemeMode.Light => Theme.Light,
                BaseThemeMode.Inherit => Theme.GetSystemTheme() switch {
                    BaseThemeMode.Dark => Theme.Dark,
                    _              => Theme.Light
                },
                _ => throw new InvalidOperationException()
            };
        }

        public static BaseThemeMode GetBaseTheme(this ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            var foreground = theme.Background.ContrastingForegroundColor();
            return foreground == Colors.Black ? BaseThemeMode.Light : BaseThemeMode.Dark;
        }

        public static void SetBaseTheme(this ITheme theme, IBaseTheme baseTheme)
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
        }

        public static void SetPrimaryColor(this ITheme theme, Color primaryColor)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.PrimaryLight = primaryColor.Lighten();
            theme.PrimaryMid = primaryColor;
            theme.PrimaryDark = primaryColor.Darken();
        }

        public static void SetSecondaryColor(this ITheme theme, Color accentColor)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            theme.SecondaryLight = accentColor.Lighten();
            theme.SecondaryMid = accentColor;
            theme.SecondaryDark = accentColor.Darken();
        }
    }
}