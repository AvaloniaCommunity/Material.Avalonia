using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Material.Styles.Assists;

/// <summary>
/// Attached properties for managing <see cref="DataValidationErrors"/>
/// </summary>
public static class DataValidationErrorsAssist {
    /// <summary>
    /// Defines font size for <see cref="DataValidationErrors"/>
    /// </summary>
    public static readonly AttachedProperty<int> FontSizeProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, int>("FontSize", typeof(DataValidationErrorsAssist), 12, true);

    /// <summary>
    /// Defines max lines for the <see cref="DataValidationErrors"/>
    /// </summary>
    public static readonly AttachedProperty<int> MaxLinesProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, int>("MaxLines", typeof(DataValidationErrorsAssist), inherits: false);

    /// <summary>
    /// Defines the number of lines for which space is always allocated even if there is no errors currently 
    /// </summary>
    public static readonly AttachedProperty<int> AlwaysAllocatedLinesProperty =
        AvaloniaProperty.RegisterAttached<TemplatedControl, int>("AlwaysAllocatedLines", typeof(DataValidationErrorsAssist), inherits: true);

    /// <summary>
    /// Sets the <see cref="FontSizeProperty"/>
    /// </summary>
    public static int GetFontSize(TemplatedControl element) {
        return element.GetValue(FontSizeProperty);
    }

    /// <summary>
    /// Gets the <see cref="FontSizeProperty"/>
    /// </summary>
    public static void SetFontSize(TemplatedControl element, int value) {
        element.SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets the <see cref="MaxLinesProperty"/>
    /// </summary>
    public static int GetMaxLines(TemplatedControl element) {
        return element.GetValue(MaxLinesProperty);
    }

    /// <summary>
    /// Sets the <see cref="MaxLinesProperty"/>
    /// </summary>
    public static void SetMaxLines(TemplatedControl element, int value) {
        element.SetValue(MaxLinesProperty, value);
    }

    /// <summary>
    /// Gets the <see cref="AlwaysAllocatedLinesProperty"/>
    /// </summary>
    public static int GetAlwaysAllocatedLines(TemplatedControl element) {
        return element.GetValue(AlwaysAllocatedLinesProperty);
    }

    /// <summary>
    /// Sets the <see cref="AlwaysAllocatedLinesProperty"/>
    /// </summary>
    public static void SetAlwaysAllocatedLines(TemplatedControl element, int value) {
        element.SetValue(AlwaysAllocatedLinesProperty, value);
    }
}