using Avalonia.Media;

namespace Material.Styles.Themes.Base;

internal sealed class MaterialDesignDarkTheme : IBaseTheme {
    public static IBaseTheme Instance { get; } = new MaterialDesignDarkTheme();

    public Color ValidationErrorColor { get; } = Color.Parse("#f44336");
    public Color MaterialBackgroundColor { get; } = Color.Parse("#FF000000");
    public Color MaterialPaperColor { get; } = Color.Parse("#FF303030");
    public Color MaterialCardBackgroundColor { get; } = Color.Parse("#FF424242");
    public Color MaterialToolBarBackgroundColor { get; } = Color.Parse("#FF212121");
    public Color MaterialBodyColor { get; } = Color.Parse("#DDFFFFFF");
    public Color MaterialBodyLightColor { get; } = Color.Parse("#89FFFFFF");
    public Color MaterialColumnHeaderColor { get; } = Color.Parse("#BCFFFFFF");
    public Color MaterialCheckBoxOffColor { get; } = Color.Parse("#89FFFFFF");
    public Color MaterialCheckBoxDisabledColor { get; } = Color.Parse("#FF647076");
    public Color MaterialTextBoxBorderColor { get; } = Color.Parse("#89FFFFFF");
    public Color MaterialDividerColor { get; } = Color.Parse("#1FFFFFFF");
    public Color MaterialSelectionColor { get; } = Color.Parse("#757575");
    public Color MaterialToolForegroundColor { get; } = Color.Parse("#FF616161");
    public Color MaterialToolBackgroundColor { get; } = Color.Parse("#FFe0e0e0");
    public Color MaterialFlatButtonClickColor { get; } = Color.Parse("#19757575");
    public Color MaterialFlatButtonRippleColor { get; } = Color.Parse("#FFB6B6B6");
    public Color MaterialToolTipBackgroundColor { get; } = Color.Parse("#eeeeee");
    public Color MaterialChipBackgroundColor { get; } = Color.Parse("#FF2E3C43");
    public Color MaterialSnackbarBackgroundColor { get; } = Color.Parse("#FFCDCDCD");
    public Color MaterialSnackbarMouseOverColor { get; } = Color.Parse("#FFB9B9BD");
    public Color MaterialSnackbarRippleColor { get; } = Color.Parse("#FF494949");
    public Color MaterialTextFieldBoxBackgroundColor { get; } = Color.Parse("#1AFFFFFF");
    public Color MaterialTextFieldBoxHoverBackgroundColor { get; } = Color.Parse("#1FFFFFFF");
    public Color MaterialTextFieldBoxDisabledBackgroundColor { get; } = Color.Parse("#0DFFFFFF");
    public Color MaterialTextAreaBorderColor { get; } = Color.Parse("#BCFFFFFF");
    public Color MaterialTextAreaInactiveBorderColor { get; } = Color.Parse("#1AFFFFFF");
    public Color MaterialDataGridRowHoverBackgroundColor { get; } = Color.Parse("#14FFFFFF");
}
