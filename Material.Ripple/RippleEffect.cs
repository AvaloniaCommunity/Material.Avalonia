using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace Material.Ripple {
    public class RippleEffect : ContentControl
    {
        // ReSharper disable once InconsistentNaming
        private Canvas PART_RippleCanvasRoot;

        private Ripple _last;
        private byte _pointers;

        public RippleEffect()
        {
            AddHandler(PointerReleasedEvent, PointerReleasedHandler);
            AddHandler(PointerPressedEvent, PointerPressedHandler);
            AddHandler(PointerCaptureLostEvent, PointerCaptureLostHandler);
        }
        
        private void PointerPressedHandler(object sender, PointerPressedEventArgs e)
        {
            if (_pointers == 0)
            {
                // Only first pointer can arrive a ripple
                _pointers++;
                var r = CreateRipple();
                _last = r;
                r.SetupInitialValues(e, this);
                    
                // Attach ripple instance to canvas
                PART_RippleCanvasRoot.Children.Add(r);
                r.RunFirstStep();
            }
        }

        private void PointerReleasedHandler(object sender, PointerReleasedEventArgs e)
        {
            RemoveLastRipple();
        }
        
        private void PointerCaptureLostHandler(object sender, PointerCaptureLostEventArgs e)
        {
            RemoveLastRipple();
        }

        private void RemoveLastRipple()
        {
            if (_last == null) 
                return;
            
            _pointers--;
                    
            // This way to handle pointer released is pretty tricky
            // could have more better way to improve
            OnReleaseHandler(_last);
            _last = null;
        }

        private void OnReleaseHandler(Ripple r)
        {
            // Fade out ripple
            r.RunSecondStep();
            
            void RemoveRippleTask(Task arg1, object arg2)
            {
                Dispatcher.UIThread.InvokeAsync(delegate
                {
                    PART_RippleCanvasRoot.Children.Remove(r);
                });
            }
            
            // Remove ripple from canvas to finalize ripple instance
            Task.Delay(Ripple.Duration).ContinueWith(RemoveRippleTask, null);
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

        public static readonly StyledProperty<IBrush> RippleFillProperty =
            AvaloniaProperty.Register<RippleEffect, IBrush>(nameof(RippleFill), SolidColorBrush.Parse("#FFF"));

        public IBrush RippleFill {
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