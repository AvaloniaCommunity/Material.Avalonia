using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Material.Styles.Controls
{
    public class ContentExpandControl : ContentControl
    {
        public static readonly StyledProperty<double> MultiplierProperty =
            AvaloniaProperty.Register<ContentExpandControl, double>(nameof(Multiplier));

        public double Multiplier
        {
            get => GetValue(MultiplierProperty);
            set => SetValue(MultiplierProperty, value);
        }

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<ContentExpandControl, Orientation>(nameof(Orientation));

        public Orientation Orientation
        {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        static ContentExpandControl()
        {
            AffectsArrange<ContentExpandControl>(MultiplierProperty, 
                OrientationProperty);
            
            AffectsMeasure<ContentExpandControl>(MultiplierProperty, 
                OrientationProperty);
        }

        protected override void ArrangeCore(Rect finalRect)
        {
            base.ArrangeCore(finalRect);
        }

        protected override Size MeasureCore(Size availableSize)
        {
            var result = base.MeasureCore(availableSize);
            return result;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var result = base.ArrangeOverride(finalSize);
            return result;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var result = base.MeasureOverride(availableSize);
            
            var w = result.Width;
            var h = result.Height;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    w *= Multiplier;
                    break;
                
                case Orientation.Vertical:
                    h *= Multiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return new Size(w, h);
        }
    }
}