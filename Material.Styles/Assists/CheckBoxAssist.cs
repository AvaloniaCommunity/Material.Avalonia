using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists {
    public static class CheckBoxAssist {
        private const double DefaultCheckBoxSize = 18.0;

        #region AttachedProperty : CheckBoxSizeProperty

        public static readonly AvaloniaProperty<double> CheckBoxSizeProperty
            = AvaloniaProperty.RegisterAttached<CheckBox, double>("CheckBoxSize", typeof(CheckBoxAssist), DefaultCheckBoxSize);

        public static double GetCheckBoxSize(CheckBox element) {
            return (double) element.GetValue(CheckBoxSizeProperty);
        }

        public static void SetCheckBoxSize(CheckBox element, double checkBoxSize) {
            element.SetValue(CheckBoxSizeProperty, checkBoxSize);
        }

        #endregion
    }
}