using Avalonia;
using Avalonia.Controls;
using Material.Styles.Enums;

namespace Material.Styles.Assists
{
    public static class PipsPagerAssist
    {
        public static readonly AttachedProperty<PipShape> PipShapeProperty =
            AvaloniaProperty.RegisterAttached<PipsPager, PipShape>("PipShape", typeof(PipsPagerAssist), PipShape.Dot);

        public static void SetPipShape(AvaloniaObject element, PipShape value) =>
            element.SetValue(PipShapeProperty, value);

        public static PipShape GetPipShape(AvaloniaObject element) =>
            element.GetValue(PipShapeProperty);

        static PipsPagerAssist() {
            PipShapeProperty.Changed.AddClassHandler<PipsPager>((pager, args) => {
                var newValue = args.GetNewValue<PipShape>();
                pager.Classes.Set("pip-dot", newValue == PipShape.Dot);
                pager.Classes.Set("pip-pill", newValue == PipShape.Pill);
                pager.Classes.Set("pip-number", newValue == PipShape.Number);
            });
        }
    }
}