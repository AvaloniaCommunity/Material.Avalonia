using Avalonia.Media;
using Material.Colors;

namespace Material.Styles.Themes
{
    public interface ITheme : IReadOnlyTheme
    {
        new ColorPair PrimaryLight { get; set; }
        new ColorPair PrimaryMid { get; set; }
        new ColorPair PrimaryDark { get; set; }

        new ColorPair SecondaryLight { get; set; }
        new ColorPair SecondaryMid { get; set; }
        new ColorPair SecondaryDark { get; set; }

        new Color ValidationError { get; set; }
        new Color Background { get; set; }
        new Color Paper { get; set; }
        new Color CardBackground { get; set; }
        new Color ToolBarBackground { get; set; }
        new Color Body { get; set; }
        new Color BodyLight { get; set; }
        new Color ColumnHeader { get; set; }

        new Color CheckBoxOff { get; set; }
        new Color CheckBoxDisabled { get; set; }

        new Color Divider { get; set; }
        new Color Selection { get; set; }

        new Color ToolForeground { get; set; }
        new Color ToolBackground { get; set; }

        new Color FlatButtonClick { get; set; }
        new Color FlatButtonRipple { get; set; }

        new Color ToolTipBackground { get; set; }
        new Color ChipBackground { get; set; }

        new Color SnackbarBackground { get; set; }
        new Color SnackbarMouseOver { get; set; }
        new Color SnackbarRipple { get; set; }

        new Color TextBoxBorder { get; set; }

        new Color TextFieldBoxBackground { get; set; }
        new Color TextFieldBoxHoverBackground { get; set; }
        new Color TextFieldBoxDisabledBackground { get; set; }
        new Color TextAreaBorder { get; set; }
        new Color TextAreaInactiveBorder { get; set; }

        new Color DataGridRowHoverBackground { get; set; }
    }
}