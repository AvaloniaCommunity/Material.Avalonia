using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Styling;

namespace RoundRipple
{
    public class RoundRippleEffect : ContentControl
    {
          #region Styled properties

        public static readonly StyledProperty<Brush> RippleFillProperty =
        AvaloniaProperty.Register<RoundRippleEffect, Brush>(nameof(RippleFill));

        public Brush RippleFill
        {
            get { return GetValue(RippleFillProperty); }
            set { SetValue(RippleFillProperty, value); }
        }

        public static readonly StyledProperty<double> RippleOpacityProperty =
       AvaloniaProperty.Register<RoundRippleEffect, double>(nameof(RippleOpacity));

        public double RippleOpacity
        {
            get { return GetValue(RippleOpacityProperty); }
            set { SetValue(RippleOpacityProperty, value); }
        }

        #endregion Styled properties

        private Ellipse _circle;
        private Animation _ripple;
        private Point _pointer;
        private IAnimationSetter _toWidth;
        private IAnimationSetter _fromMargin;
        private IAnimationSetter _toMargin;
        private bool _isRunning;

        public RoundRippleEffect()
        {
            this.AddHandler(PointerPressedEvent, async (s, e) =>
            {
                if (_isRunning)
                {
                    return;
                }
                _pointer = e.GetPosition(this);
                _isRunning = true;
                var maxWidth = Math.Max(Bounds.Width, Bounds.Width) * 2.2D;
                _toWidth.Value = maxWidth;
                _fromMargin.Value = _circle.Margin = new Thickness(_pointer.X, _pointer.Y, 0, 0);
                _toMargin.Value = new Thickness(_pointer.X - maxWidth / 2, _pointer.Y - maxWidth / 2, 0, 0);

                await _ripple.RunAsync(_circle);

                _isRunning = false;
            });
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _circle = e.NameScope.Find<Ellipse>("Circle");

            var style = _circle.Styles[0] as Style;
            _ripple = style.Animations[0] as Animation;
            _toWidth = _ripple.Children[1].Setters[1];
            _fromMargin = _ripple.Children[0].Setters[0];
            _toMargin = _ripple.Children[1].Setters[0];

            style.Animations.Remove(_ripple);
        }
    }
}