using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Material.Ripple
{
    public class Ripple : Ellipse
    {
        private static Easing _easing = new CircularEaseOut();
        public static Easing Easing
        {
            get => _easing;
            set => _easing = value;
        }
        
        public static readonly TimeSpan Duration = new TimeSpan(0,0,0,0,500);

        private double maxDiam;

        private double endX, endY;
        
        public Ripple(double outerWidth, double outerHeight)
        {
            maxDiam = Math.Sqrt(Math.Pow(outerWidth, 2) + Math.Pow(outerHeight, 2));
            endY = maxDiam - outerHeight;
            endX = maxDiam - outerWidth;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Opacity = 1;
        }

        public void SetupInitialValues(PointerPressedEventArgs e, Control parent)
        {
            Width = 0;
            Height = 0;
            var pointer = e.GetPosition(parent);
            Margin = new Thickness(pointer.X, pointer.Y, 0, 0);
        }

        public void RunFirstStep()
        {
            Width = maxDiam;
            Height = maxDiam;
            Margin = new Thickness(-endX / 2, -endY / 2, 0, 0);
        }

        public void RunSecondStep()
        {
            Opacity = 0;
        }
    }
}