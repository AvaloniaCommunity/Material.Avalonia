using System;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Styling;

namespace Material.Ripple
{
    public class RippleEffect : ContentControl
    {
        private Ellipse _circle;
        private IAnimationSetter _fromMargin;
        private bool _isRunning;
        private Point _pointer;
        private Animation _ripple;
        private IAnimationSetter _toMargin;
        private IAnimationSetter _toWidth;

        public RippleEffect()
        {
            AddHandler(PointerPressedEvent, async (s, e) =>
            {
                if (_isRunning) return;
                _pointer = e.GetPosition(this);
                _isRunning = true;
                var maxWidth = Math.Max(Bounds.Width, Bounds.Height) * 2.2D;
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

        #region Styled properties

        public static readonly StyledProperty<Brush> RippleFillProperty =
            AvaloniaProperty.Register<RippleEffect, Brush>(nameof(RippleFill));

        public Brush RippleFill
        {
            get => GetValue(RippleFillProperty);
            set => SetValue(RippleFillProperty, value);
        }

        public static readonly StyledProperty<double> RippleOpacityProperty =
            AvaloniaProperty.Register<RippleEffect, double>(nameof(RippleOpacity));

        public double RippleOpacity
        {
            get => GetValue(RippleOpacityProperty);
            set => SetValue(RippleOpacityProperty, value);
        }

        #endregion Styled properties
    }
}