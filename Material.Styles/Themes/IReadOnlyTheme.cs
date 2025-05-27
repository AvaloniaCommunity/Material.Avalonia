using Avalonia.Media;
using Material.Colors;

namespace Material.Styles.Themes;

public interface IReadOnlyTheme {
    ColorPair PrimaryLight { get; }
    ColorPair PrimaryMid { get; }
    ColorPair PrimaryDark { get; }

    ColorPair SecondaryLight { get; }
    ColorPair SecondaryMid { get; }
    ColorPair SecondaryDark { get; }

    ColorPair ValidationError { get; }

    Color Background { get; }
    Color Paper { get; }
    Color CardBackground { get; }
    Color ToolBarBackground { get; }
    Color Body { get; }
    Color BodyLight { get; }
    Color ColumnHeader { get; }

    Color CheckBoxOff { get; }
    Color CheckBoxDisabled { get; }

    Color Divider { get; }
    Color Selection { get; }

    Color ToolForeground { get; }
    Color ToolBackground { get; }

    Color FlatButtonClick { get; }
    Color FlatButtonRipple { get; }

    Color ToolTipBackground { get; }
    Color ChipBackground { get; }

    Color SnackbarBackground { get; }
    Color SnackbarMouseOver { get; }
    Color SnackbarRipple { get; }

    Color TextBoxBorder { get; }

    Color TextFieldBoxBackground { get; }
    Color TextFieldBoxHoverBackground { get; }
    Color TextFieldBoxDisabledBackground { get; }
    Color TextAreaBorder { get; }
    Color TextAreaInactiveBorder { get; }

    Color DataGridRowHoverBackground { get; }
}
