using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace Material.Ripple {
    public class RippleEffect : ContentControl
    {
        private Canvas PART_RippleCanvasRoot;

        private Ripple last;
        private short pointers;

        public RippleEffect()
        {
            AddHandler(PointerReleasedEvent, (s, e) => {
                if (last != null)
                {
                    pointers--;
                    
                    // This way to handle pointer released is pretty tricky
                    // could have more better way to improve
                    OnReleaseHandler(last, e);
                    last = null;
                }
            });
            AddHandler(PointerPressedEvent, async (s, e) => {
                if (pointers == 0)
                {
                    // Only first pointer can arrive a ripple
                    pointers++;
                    var r = CreateRipple();
                    last = r;
                    r.SetupInitialValues(e, this);
                    
                    // Attach ripple instance to canvas
                    PART_RippleCanvasRoot.Children.Add(r);
                    r.RunFirstStep(e, this);
                }
            });
        }

        private void OnReleaseHandler(object sender, PointerReleasedEventArgs e)
        {
            var r = sender as Ripple;
            
            // Fade out ripple
            r.RunSecondStep(e);
            
            // Remove ripple from canvas to finalize ripple instance
            Task.Delay(Ripple.Duration).ContinueWith((a, b) =>
            {
                Dispatcher.UIThread.InvokeAsync(() => PART_RippleCanvasRoot.Children.Remove(r));
            }, null);
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);

            // Find canvas host
            PART_RippleCanvasRoot = e.NameScope.Find<Canvas>(nameof(PART_RippleCanvasRoot));
        }

        private Ripple CreateRipple()
        {
            return new Ripple(Bounds.Width, Bounds.Height)
            {
                Fill = RippleFill
            };
        }

        #region Styled properties

        public static readonly StyledProperty<Brush> RippleFillProperty =
            AvaloniaProperty.Register<RippleEffect, Brush>(nameof(RippleFill), SolidColorBrush.Parse("#FFF"));

        public Brush RippleFill {
            get => GetValue(RippleFillProperty);
            set => SetValue(RippleFillProperty, value);
        }

        public static readonly StyledProperty<double> RippleOpacityProperty =
            AvaloniaProperty.Register<RippleEffect, double>(nameof(RippleOpacity), 0.6);

        public double RippleOpacity {
            get => GetValue(RippleOpacityProperty);
            set => SetValue(RippleOpacityProperty, value);
        }

        #endregion Styled properties
    }
}