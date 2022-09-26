using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;

namespace Material.Styles.Controls
{
    [PseudoClasses(":selected", ":dot")]
    public class CircleClockPickerCell : ContentControl
    {
        public static readonly StyledProperty<bool> IsSelectedProperty =
            AvaloniaProperty.Register<CircleClockPickerCell, bool>(nameof(IsSelected));

        public static readonly StyledProperty<bool> IsDotProperty =
            AvaloniaProperty.Register<CircleClockPickerCell, bool>(nameof(IsDot));

        public static readonly DirectProperty<CircleClockPickerCell, int> ValueProperty =
            AvaloniaProperty.RegisterDirect<CircleClockPickerCell, int>(nameof(Value), o => o.Value,
                (o, v) => o.Value = v);

        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public bool IsDot
        {
            get => GetValue(IsDotProperty);
            set => SetValue(IsDotProperty, value);
        }

        public int Value
        {
            get => _value;
            set => SetAndRaise(ValueProperty, ref _value, value);
        }

        private int _value;

        static CircleClockPickerCell()
        {
            AffectsArrange<CircleClockPickerCell>(IsDotProperty);
            AffectsRender<CircleClockPickerCell>(IsSelectedProperty);

            IsSelectedProperty.Changed.AddClassHandler<CircleClockPickerCell>(PropertyChangedHandler);
            IsDotProperty.Changed.AddClassHandler<CircleClockPickerCell>(PropertyChangedHandler);
        }

        private static void PropertyChangedHandler(CircleClockPickerCell t,
            AvaloniaPropertyChangedEventArgs a)
        {
            t.UpdatePseudoClasses();
        }

        private void UpdatePseudoClasses()
        {
            PseudoClasses.Set(":selected", IsSelected);
            PseudoClasses.Set(":dot", IsDot);
        }
    }
}