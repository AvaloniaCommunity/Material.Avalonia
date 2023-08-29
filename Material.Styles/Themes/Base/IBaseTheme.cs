using Avalonia.Media;

namespace Material.Styles.Themes.Base
{
    public interface IBaseTheme
    {
        Color MaterialValidationErrorColor { get; }
        Color MaterialBackgroundColor { get; }
        Color MaterialPaperColor { get; }
        Color MaterialCardBackgroundColor { get; }
        Color MaterialToolBarBackgroundColor { get; }
        Color MaterialBodyColor { get; }
        Color MaterialBodyLightColor { get; }
        Color MaterialColumnHeaderColor { get; }
        Color MaterialCheckBoxOffColor { get; }
        Color MaterialCheckBoxDisabledColor { get; }
        Color MaterialTextBoxBorderColor { get; }
        Color MaterialDividerColor { get; }
        Color MaterialSelectionColor { get; }
        Color MaterialToolForegroundColor { get; }
        Color MaterialToolBackgroundColor { get; }
        Color MaterialFlatButtonClickColor { get; }
        Color MaterialFlatButtonRippleColor { get; }
        Color MaterialToolTipBackgroundColor { get; }
        Color MaterialChipBackgroundColor { get; }
        Color MaterialSnackbarBackgroundColor { get; }
        Color MaterialSnackbarMouseOverColor { get; }
        Color MaterialSnackbarRippleColor { get; }
        Color MaterialTextFieldBoxBackgroundColor { get; }
        Color MaterialTextFieldBoxHoverBackgroundColor { get; }
        Color MaterialTextFieldBoxDisabledBackgroundColor { get; }
        Color MaterialTextAreaBorderColor { get; }
        Color MaterialTextAreaInactiveBorderColor { get; }
        Color MaterialDataGridRowHoverBackgroundColor { get; }
    }
}