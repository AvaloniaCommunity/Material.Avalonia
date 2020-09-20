using System;
using Avalonia;
using Avalonia.Controls;

namespace MaterialXamlToolKit.Avalonia.Assists {
    public static class CheckBoxAssist {
        private const double DefaultCheckBoxSize = 18.0;

        #region AttachedProperty : CheckBoxSizeProperty

        public static readonly AvaloniaProperty<double> CheckBoxSizeProperty
            = AvaloniaProperty.RegisterAttached<CheckBox, double>("CheckBoxSize", typeof(CheckBoxAssist), DefaultCheckBoxSize);

        public static double GetCheckBoxSize(CheckBox element) => (double) element.GetValue(CheckBoxSizeProperty);
        public static void SetCheckBoxSize(CheckBox element, double checkBoxSize) => element.SetValue(CheckBoxSizeProperty, checkBoxSize);

        #endregion
    }
}