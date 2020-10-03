using Avalonia;
using Avalonia.Controls;

namespace MaterialXamlToolKit.Avalonia.Assists {
    public static class ButtonAssist {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

        #region AttachedProperty : CornerRadiusProperty

        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly AvaloniaProperty<CornerRadius> CornerRadiusProperty = AvaloniaProperty.RegisterAttached<Button, CornerRadius>(
            "CornerRadius", typeof(ButtonAssist), DefaultCornerRadius, true);

        public static CornerRadius GetCornerRadius(AvaloniaObject element) => (CornerRadius) element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(AvaloniaObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        #endregion
    }
}