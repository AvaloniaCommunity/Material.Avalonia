using System;
using Avalonia.Media;
using Material.Colors;
using Material.Styles.Themes.Base;

namespace Material.Styles.Themes {
    public class Theme : ITheme {
        public static IBaseTheme Light { get; } = MaterialDesignLightTheme.Instance;
        public static IBaseTheme Dark { get; } = MaterialDesignDarkTheme.Instance;

        public ColorPair SecondaryLight { get; set; }
        public ColorPair SecondaryMid { get; set; }
        public ColorPair SecondaryDark { get; set; }

        public ColorPair PrimaryLight { get; set; }
        public ColorPair PrimaryMid { get; set; }
        public ColorPair PrimaryDark { get; set; }

        public Color ValidationError { get; set; }
        public Color Background { get; set; }
        public Color Paper { get; set; }
        public Color CardBackground { get; set; }
        public Color ToolBarBackground { get; set; }
        public Color Body { get; set; }
        public Color BodyLight { get; set; }
        public Color ColumnHeader { get; set; }
        public Color CheckBoxOff { get; set; }
        public Color CheckBoxDisabled { get; set; }
        public Color Divider { get; set; }
        public Color Selection { get; set; }
        public Color ToolForeground { get; set; }
        public Color ToolBackground { get; set; }
        public Color FlatButtonClick { get; set; }
        public Color FlatButtonRipple { get; set; }
        public Color ToolTipBackground { get; set; }
        public Color ChipBackground { get; set; }
        public Color SnackbarBackground { get; set; }
        public Color SnackbarMouseOver { get; set; }
        public Color SnackbarRipple { get; set; }
        public Color TextBoxBorder { get; set; }
        public Color TextFieldBoxBackground { get; set; }
        public Color TextFieldBoxHoverBackground { get; set; }
        public Color TextFieldBoxDisabledBackground { get; set; }
        public Color TextAreaBorder { get; set; }
        public Color TextAreaInactiveBorder { get; set; }
        public Color DataGridRowHoverBackground { get; set; }

        public static Theme Create(IBaseTheme baseTheme, Color primary, Color accent) {
            if (baseTheme is null) throw new ArgumentNullException(nameof(baseTheme));
            var theme = new Theme();

            theme.SetBaseTheme(baseTheme);
            theme.SetPrimaryColor(primary);
            theme.SetSecondaryColor(accent);

            return theme;
        }

        public static Theme Create(IReadOnlyTheme readOnlyTheme) {
            if (readOnlyTheme is null) throw new ArgumentNullException(nameof(readOnlyTheme));
            var theme = new Theme {
                PrimaryLight = readOnlyTheme.PrimaryLight,
                PrimaryMid = readOnlyTheme.PrimaryMid,
                PrimaryDark = readOnlyTheme.PrimaryDark,
                SecondaryLight = readOnlyTheme.SecondaryLight,
                SecondaryMid = readOnlyTheme.SecondaryMid,
                SecondaryDark = readOnlyTheme.SecondaryDark,
                ValidationError = readOnlyTheme.ValidationError,
                Background = readOnlyTheme.Background,
                Paper = readOnlyTheme.Paper,
                CardBackground = readOnlyTheme.CardBackground,
                ToolBarBackground = readOnlyTheme.ToolBarBackground,
                Body = readOnlyTheme.Body,
                BodyLight = readOnlyTheme.BodyLight,
                ColumnHeader = readOnlyTheme.ColumnHeader,
                CheckBoxOff = readOnlyTheme.CheckBoxOff,
                CheckBoxDisabled = readOnlyTheme.CheckBoxDisabled,
                Divider = readOnlyTheme.Divider,
                Selection = readOnlyTheme.Selection,
                ToolForeground = readOnlyTheme.ToolForeground,
                ToolBackground = readOnlyTheme.ToolBackground,
                FlatButtonClick = readOnlyTheme.FlatButtonClick,
                FlatButtonRipple = readOnlyTheme.FlatButtonRipple,
                ToolTipBackground = readOnlyTheme.ToolTipBackground,
                ChipBackground = readOnlyTheme.ChipBackground,
                SnackbarBackground = readOnlyTheme.SnackbarBackground,
                SnackbarMouseOver = readOnlyTheme.SnackbarMouseOver,
                SnackbarRipple = readOnlyTheme.SnackbarRipple,
                TextBoxBorder = readOnlyTheme.TextBoxBorder,
                TextFieldBoxBackground = readOnlyTheme.TextFieldBoxBackground,
                TextFieldBoxHoverBackground = readOnlyTheme.TextFieldBoxHoverBackground,
                TextFieldBoxDisabledBackground = readOnlyTheme.TextFieldBoxDisabledBackground,
                TextAreaBorder = readOnlyTheme.TextAreaBorder,
                TextAreaInactiveBorder = readOnlyTheme.TextAreaInactiveBorder,
                DataGridRowHoverBackground = readOnlyTheme.DataGridRowHoverBackground
            };

            return theme;
        }
    }
}
