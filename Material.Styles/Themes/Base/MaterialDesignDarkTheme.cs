using Avalonia.Media;

namespace Material.Styles.Themes.Base {
    public class MaterialDesignDarkTheme : IBaseTheme {
        public Color ValidationErrorColor { get; } = Color.Parse("#f44336");
        public Color MaterialDesignBackground { get; } = Color.Parse("#FF000000");
        public Color MaterialDesignPaper { get; } = Color.Parse("#FF303030");
        public Color MaterialDesignCardBackground { get; } = Color.Parse("#FF424242");
        public Color MaterialDesignToolBarBackground { get; } = Color.Parse("#FF212121");
        public Color MaterialDesignBody { get; } = Color.Parse("#DDFFFFFF");
        public Color MaterialDesignBodyLight { get; } = Color.Parse("#89FFFFFF");
        public Color MaterialDesignColumnHeader { get; } = Color.Parse("#BCFFFFFF");
        public Color MaterialDesignCheckBoxOff { get; } = Color.Parse("#89FFFFFF");
        public Color MaterialDesignCheckBoxDisabled { get; } = Color.Parse("#FF647076");
        public Color MaterialDesignTextBoxBorder { get; } = Color.Parse("#89FFFFFF");
        public Color MaterialDesignDivider { get; } = Color.Parse("#1FFFFFFF");
        public Color MaterialDesignSelection { get; } = Color.Parse("#757575");
        public Color MaterialDesignToolForeground { get; } = Color.Parse("#FF616161");
        public Color MaterialDesignToolBackground { get; } = Color.Parse("#FFe0e0e0");
        public Color MaterialDesignFlatButtonClick { get; } = Color.Parse("#19757575");
        public Color MaterialDesignFlatButtonRipple { get; } = Color.Parse("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground { get; } = Color.Parse("#eeeeee");
        public Color MaterialDesignChipBackground { get; } = Color.Parse("#FF2E3C43");
        public Color MaterialDesignSnackbarBackground { get; } = Color.Parse("#FFCDCDCD");
        public Color MaterialDesignSnackbarMouseOver { get; } = Color.Parse("#FFB9B9BD");
        public Color MaterialDesignSnackbarRipple { get; } = Color.Parse("#FF494949");
        public Color MaterialDesignTextFieldBoxBackground { get; } = Color.Parse("#1AFFFFFF");
        public Color MaterialDesignTextFieldBoxHoverBackground { get; } = Color.Parse("#1FFFFFFF");
        public Color MaterialDesignTextFieldBoxDisabledBackground { get; } = Color.Parse("#0DFFFFFF");
        public Color MaterialDesignTextAreaBorder { get; } = Color.Parse("#BCFFFFFF");
        public Color MaterialDesignTextAreaInactiveBorder { get; } = Color.Parse("#1AFFFFFF");
        public Color MaterialDesignDataGridRowHoverBackground { get; } = Color.Parse("#14FFFFFF");
    }
}