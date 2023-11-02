using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists;

public static class TextBoxAssist {
    /// <summary>
    /// Allows to compensate TextBox margins to allow InnerRightContent to be displayed without TextBox padding
    /// </summary>
    public static readonly AttachedProperty<bool> CompensateInnerRightContentMarginProperty =
        AvaloniaProperty.RegisterAttached<TextBox, bool>("CompensateInnerRightContentMargin", typeof(TextBoxAssist));

    /// <summary>
    /// Allows to compensate TextBox margins to allow InnerLeftContent to be displayed without TextBox padding
    /// </summary>
    public static readonly AttachedProperty<bool> CompensateInnerLeftContentMarginProperty =
        AvaloniaProperty.RegisterAttached<TextBox, bool>("CompensateInnerLeftContentMargin", typeof(TextBoxAssist));

    /// <summary>
    /// Gets the <see cref="CompensateInnerRightContentMarginProperty"/>
    /// </summary>
    public static bool GetCompensateInnerRightContentMargin(TextBox element) {
        return element.GetValue(CompensateInnerRightContentMarginProperty);
    }

    /// <summary>
    /// Sets the <see cref="CompensateInnerRightContentMarginProperty"/>
    /// </summary>
    public static void SetCompensateInnerRightContentMargin(TextBox element, bool value) {
        element.SetValue(CompensateInnerRightContentMarginProperty, value);
    }

    /// <summary>
    /// Gets the <see cref="CompensateInnerLeftContentMarginProperty"/>
    /// </summary>
    public static bool GetCompensateInnerLeftContentMargin(TextBox element) {
        return element.GetValue(CompensateInnerLeftContentMarginProperty);
    }

    /// <summary>
    /// Sets the <see cref="CompensateInnerLeftContentMarginProperty"/>
    /// </summary>
    public static void SetCompensateInnerLeftContentMargin(TextBox element, bool value) {
        element.SetValue(CompensateInnerLeftContentMarginProperty, value);
    }
}