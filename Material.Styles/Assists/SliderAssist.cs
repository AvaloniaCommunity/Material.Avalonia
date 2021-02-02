using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists
{
    public static class SliderAssist
    {
        private static readonly double DefaultThicknessTick = 1.0;
        private static readonly double DefaultSizeTick = 1.0;

        #region AttachedProperty
        
        public static readonly AvaloniaProperty<double> ThicknessTickProperty = AvaloniaProperty.RegisterAttached<Slider, double>(
            "ThicknessTick", typeof(SliderAssist), DefaultThicknessTick, true);

        public static double GetThicknessTick(AvaloniaObject element) {
            return (double) element.GetValue(ThicknessTickProperty);
        }

        public static void SetThicknessTick(AvaloniaObject element, double value) {
            element.SetValue(ThicknessTickProperty, value);
        }
        
        
        public static readonly AvaloniaProperty<double> SizeTickProperty = AvaloniaProperty.RegisterAttached<Slider, double>(
            "SizeTick", typeof(SliderAssist), DefaultSizeTick, true);

        public static double GetSizeTick(AvaloniaObject element) {
            return (double) element.GetValue(SizeTickProperty);
        }

        public static void SetSizeTick(AvaloniaObject element, double value) {
            element.SetValue(SizeTickProperty, value);
        }


        #endregion
    }
}