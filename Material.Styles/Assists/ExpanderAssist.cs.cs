using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists;

public static class ExpanderAssist {
    public static readonly AttachedProperty<double> HeaderHeightProperty =
        AvaloniaProperty.RegisterAttached<Expander, double>(
            "HeaderHeight", typeof(Expander), 48);

    public static void SetHeaderHeight(AvaloniaObject element, double value) =>
        element.SetValue(HeaderHeightProperty, value);

    public static double GetHeaderHeight(AvaloniaObject element) =>
        element.GetValue(HeaderHeightProperty);
}