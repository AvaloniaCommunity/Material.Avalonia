using Avalonia.Media;

namespace Material.Styles.Themes.Base;

internal sealed class MaterialDesignLightTheme : IBaseTheme {
    public static IBaseTheme Instance { get; } = new MaterialDesignLightTheme();

    public Color MaterialValidationErrorColor { get; } = Color.Parse("#F44336");
    public Color MaterialBackgroundColor { get; } = Color.Parse("#FFFFFFFF");
    public Color MaterialPaperColor { get; } = Color.Parse("#FFFAFAFA");
    public Color MaterialCardBackgroundColor { get; } = Color.Parse("#FFFFFFFF");
    public Color MaterialToolBarBackgroundColor { get; } = Color.Parse("#FFF5F5F5");
    public Color MaterialBodyColor { get; } = Color.Parse("#DD000000");
    public Color MaterialBodyLightColor { get; } = Color.Parse("#89000000");
    public Color MaterialColumnHeaderColor { get; } = Color.Parse("#BC000000");
    public Color MaterialCheckBoxOffColor { get; } = Color.Parse("#89000000");
    public Color MaterialCheckBoxDisabledColor { get; } = Color.Parse("#FFBDBDBD");
    public Color MaterialTextBoxBorderColor { get; } = Color.Parse("#89000000");
    public Color MaterialDividerColor { get; } = Color.Parse("#1F000000");
    public Color MaterialSelectionColor { get; } = Color.Parse("#FFDEDEDE");
    public Color MaterialToolForegroundColor { get; } = Color.Parse("#FF616161");
    public Color MaterialToolBackgroundColor { get; } = Color.Parse("#FFE0E0E0");
    public Color MaterialFlatButtonClickColor { get; } = Color.Parse("#FFDEDEDE");
    public Color MaterialFlatButtonRippleColor { get; } = Color.Parse("#FFB6B6B6");
    public Color MaterialToolTipBackgroundColor { get; } = Color.Parse("#757575");
    public Color MaterialChipBackgroundColor { get; } = Color.Parse("#12000000");
    public Color MaterialSnackbarBackgroundColor { get; } = Color.Parse("#FF323232");
    public Color MaterialSnackbarMouseOverColor { get; } = Color.Parse("#FF464642");
    public Color MaterialSnackbarRippleColor { get; } = Color.Parse("#FFB6B6B6");
    public Color MaterialTextFieldBoxBackgroundColor { get; } = Color.Parse("#0F000000");
    public Color MaterialTextFieldBoxHoverBackgroundColor { get; } = Color.Parse("#14000000");
    public Color MaterialTextFieldBoxDisabledBackgroundColor { get; } = Color.Parse("#08000000");
    public Color MaterialTextAreaBorderColor { get; } = Color.Parse("#BC000000");
    public Color MaterialTextAreaInactiveBorderColor { get; } = Color.Parse("#0F000000");
    public Color MaterialDataGridRowHoverBackgroundColor { get; } = Color.Parse("#0A000000");
}
