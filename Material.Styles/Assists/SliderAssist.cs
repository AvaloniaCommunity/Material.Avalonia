using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists
{
    public static class SliderAssist
    {
        private static readonly double DefaultThicknessTick = 1.0;

        #region AttachedProperty

        public static readonly AvaloniaProperty<double?> ThicknessTickProperty =
            AvaloniaProperty.RegisterAttached<Slider, double?>(
                "ThicknessTick", typeof(SliderAssist), DefaultThicknessTick, true);

        public static double? GetThicknessTick(AvaloniaObject element) =>
            element.GetValue<double?>(ThicknessTickProperty);

        public static void SetThicknessTick(AvaloniaObject element, double? value) =>
            element.SetValue(ThicknessTickProperty, value);

        #endregion
    }
}