using System;
using Avalonia;
using Avalonia.Controls;

namespace Material.Styles.Assists
{
    public enum PipShape
    {
        Dot,
        Pill,
        Number
    }

    public static class PipsPagerAssist
    {
        #region AttachedProperty : PipShape

        public static readonly AttachedProperty<PipShape> PipShapeProperty =
            AvaloniaProperty.RegisterAttached<PipsPager, PipShape>("PipShape", typeof(PipsPagerAssist), PipShape.Dot);

        public static void SetPipShape(AvaloniaObject element, PipShape value) =>
            element.SetValue(PipShapeProperty, value);

        public static PipShape GetPipShape(AvaloniaObject element) =>
            element.GetValue(PipShapeProperty);

        #endregion

        static PipsPagerAssist()
        {
            PipShapeProperty.Changed.AddClassHandler<PipsPager>((pager, args) =>
            {
                pager.Classes.Remove("pip-dot");
                pager.Classes.Remove("pip-pill");
                pager.Classes.Remove("pip-number");

                switch (args.GetNewValue<PipShape>())
                {
                    case PipShape.Dot:
                        pager.Classes.Add("pip-dot");
                        break;
                    case PipShape.Pill:
                        pager.Classes.Add("pip-pill");
                        break;
                    case PipShape.Number:
                        pager.Classes.Add("pip-number");
                        break;
                }
            });
        }
    }
}
