using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;

namespace Material.Ripple {
    public class RippleEffect : ContentControl {
        private Ellipse _circle;
        private IAnimationSetter _fromMargin;
        private bool _isRunning;
        private Animation _ripple;
        private IAnimationSetter _toMargin;
        private IAnimationSetter _toWidth;

        public RippleEffect() {
            AddHandler(PointerReleasedEvent, async (s, e) => {
                if (Classes.Contains("notransitions"))
                    NoTransitionsReleased();
            });
            AddHandler(PointerPressedEvent, async (s, e) => {
                if (_isRunning) return;
                if (Classes.Contains("notransitions"))
                {
                    NoTransitionsPressed(e);
                    return;
                }
                var pointer = e.GetPosition(this);
                _isRunning = true;
                var maxWidth = Math.Max(Bounds.Width, Bounds.Height) * 2.2D;
                _toWidth.Value = maxWidth;
                _fromMargin.Value = _circle.Margin = new Thickness(pointer.X, pointer.Y, 0, 0);
                _toMargin.Value = new Thickness(pointer.X - maxWidth / 2, pointer.Y - maxWidth / 2, 0, 0);
                _circle.Opacity = 0;
                await _ripple.RunAsync(_circle);
                _circle.Opacity = 0;
                _isRunning = false;
            });
        }
        
        private void NoTransitionsReleased()
        {
            _circle.Opacity = 0;
            _circle.Width = 0;
        }
        private void NoTransitionsPressed(PointerPressedEventArgs e)
        {
            var pointer = e.GetPosition(this);
            _circle.Opacity = 1;
            _circle.Width = Math.Max(Bounds.Width, Bounds.Height) * 2.2D;
            _circle.Margin = new Thickness(pointer.X - _circle.Width / 2, pointer.Y - _circle.Width / 2, 0, 0);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);
            _circle = e.NameScope.Find<Ellipse>("Circle");

            var style = _circle.Styles[0] as Style;
            _ripple = style.Animations[0] as Animation;
            _toWidth = _ripple.Children[2].Setters[1];
            _fromMargin = _ripple.Children[0].Setters[0];
            _toMargin = _ripple.Children[2].Setters[0];

            style.Animations.Remove(_ripple);
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