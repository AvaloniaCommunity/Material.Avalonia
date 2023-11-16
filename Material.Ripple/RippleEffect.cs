using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.Threading;

namespace Material.Ripple {
    public class RippleEffect : ContentControl {

        private bool _isCancelled;

        private CompositionContainerVisual? _container;
        private CompositionCustomVisual? _last;
        private byte _pointers;
        
        static RippleEffect() {
            BackgroundProperty.OverrideDefaultValue<RippleEffect>(Brushes.Transparent);
        }
        
        public RippleEffect() {
            AddHandler(LostFocusEvent, LostFocusHandler);
            AddHandler(PointerReleasedEvent, PointerReleasedHandler);
            AddHandler(PointerPressedEvent, PointerPressedHandler);
            AddHandler(PointerCaptureLostEvent, PointerCaptureLostHandler);
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
            base.OnAttachedToVisualTree(e);

            var thisVisual = ElementComposition.GetElementVisual(this)!;
            _container = thisVisual.Compositor.CreateContainerVisual();
            _container.Size = new Vector(Bounds.Width, Bounds.Height);
            ElementComposition.SetElementChildVisual(this, _container);
        }

        protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
            base.OnDetachedFromVisualTree(e);

            _container = null;
            ElementComposition.SetElementChildVisual(this, null);
        }

        protected override void OnSizeChanged(SizeChangedEventArgs e) {
            base.OnSizeChanged(e);

            if (_container is { } container) {
                var newSize = new Vector(e.NewSize.Width, e.NewSize.Height);
                if (newSize != default) {
                    container.Size = newSize;
                    foreach (var child in container.Children) {
                        child.Size = newSize;
                    }
                }
            }
        }

        private void PointerPressedHandler(object sender, PointerPressedEventArgs e) {
            var (x, y) = e.GetPosition(this);
            if (_container is null || x < 0 || x > Bounds.Width || y < 0 || y > Bounds.Height) {
                return;
            }
            _isCancelled = false;

            if (!IsAllowedRaiseRipple)
                return;

            if (_pointers != 0)
                return;

            // Only first pointer can arrive a ripple
            _pointers++;
            var r = CreateRipple(x, y, RaiseRippleCenter);
            _last = r;

            // Attach ripple instance to canvas
            _container.Children.Add(r);
            r.SendHandlerMessage(RippleHandler.FirstStepMessage);

            if (_isCancelled) {
                RemoveLastRipple();
            }
        }

        private void LostFocusHandler(object sender, RoutedEventArgs e) {
            _isCancelled = true;
            RemoveLastRipple();
        }

        private void PointerReleasedHandler(object sender, PointerReleasedEventArgs e) {
            _isCancelled = true;
            RemoveLastRipple();
        }

        private void PointerCaptureLostHandler(object sender, PointerCaptureLostEventArgs e) {
            _isCancelled = true;
            RemoveLastRipple();
        }

        private void RemoveLastRipple() {
            if (_last == null)
                return;

            _pointers--;

            // This way to handle pointer released is pretty tricky
            // could have more better way to improve
            OnReleaseHandler(_last);
            _last = null;
        }

        private void OnReleaseHandler(CompositionCustomVisual r) {
            // Fade out ripple
            r.SendHandlerMessage(RippleHandler.SecondStepMessage);

            // Remove ripple from canvas to finalize ripple instance
            var container = _container;
            DispatcherTimer.RunOnce(() => {
                container?.Children.Remove(r);
            }, Ripple.Duration, DispatcherPriority.Render);
        }

        private CompositionCustomVisual CreateRipple(double x, double y, bool center) {
            var w = Bounds.Width;
            var h = Bounds.Height;
            var t = UseTransitions;

            if (center) {
                x = w / 2;
                y = h / 2;
            }
            
            var handler = new RippleHandler(
                RippleFill.ToImmutable(),
                Ripple.Easing,
                Ripple.Duration,
                RippleOpacity,
                x, y, w, h, t);

            var visual = ElementComposition.GetElementVisual(this)!.Compositor.CreateCustomVisual(handler);
            visual.Size = new Vector(Bounds.Width, Bounds.Height);
            return visual;
        }

        #region Styled properties

        public static readonly StyledProperty<IBrush> RippleFillProperty =
            AvaloniaProperty.Register<RippleEffect, IBrush>(nameof(RippleFill), inherits: true, defaultValue: Brushes.White);

        public IBrush RippleFill {
            get => GetValue(RippleFillProperty);
            set => SetValue(RippleFillProperty, value);
        }

        public static readonly StyledProperty<double> RippleOpacityProperty =
            AvaloniaProperty.Register<RippleEffect, double>(nameof(RippleOpacity), inherits: true, defaultValue: 0.6);

        public double RippleOpacity {
            get => GetValue(RippleOpacityProperty);
            set => SetValue(RippleOpacityProperty, value);
        }

        public static readonly StyledProperty<bool> RaiseRippleCenterProperty =
            AvaloniaProperty.Register<RippleEffect, bool>(nameof(RaiseRippleCenter));

        public bool RaiseRippleCenter {
            get => GetValue(RaiseRippleCenterProperty);
            set => SetValue(RaiseRippleCenterProperty, value);
        }

        public static readonly StyledProperty<bool> IsAllowedRaiseRippleProperty =
            AvaloniaProperty.Register<RippleEffect, bool>(nameof(IsAllowedRaiseRipple), defaultValue: true);

        public bool IsAllowedRaiseRipple {
            get => GetValue(IsAllowedRaiseRippleProperty);
            set => SetValue(IsAllowedRaiseRippleProperty, value);
        }

        public static readonly StyledProperty<bool> UseTransitionsProperty =
            AvaloniaProperty.Register<RippleEffect, bool>(nameof(UseTransitions), defaultValue: true);

        public bool UseTransitions {
            get => GetValue(UseTransitionsProperty);
            set => SetValue(UseTransitionsProperty, value);
        }
        
        #endregion Styled properties
    }
}