using System;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Rendering.Composition;

namespace Material.Ripple;

internal class RippleHandler : CompositionCustomVisualHandler {
    public static readonly object FirstStepMessage = new(), SecondStepMessage = new();

    private readonly IImmutableBrush _brush;
    private readonly Point _center;
    private readonly TimeSpan _duration;
    private readonly Easing _easing;

    private readonly double _maxRadius;
    private readonly double _opacity;
    private readonly RoundedRect _cornerRadiusRect;
    private readonly bool _transitions;
    private TimeSpan _animationElapsed;
    private TimeSpan? _lastServerTime;
    private TimeSpan? _secondStepStart;

    public RippleHandler(IImmutableBrush brush,
        Easing easing,
        TimeSpan duration,
        double opacity,
        CornerRadius cornerRadius,
        double positionX, double positionY,
        double outerWidth, double outerHeight, bool transitions) {
        _brush = brush;
        _easing = easing;
        _duration = duration;
        _opacity = opacity;
        _cornerRadiusRect = new RoundedRect(new Rect(0, 0, outerWidth, outerHeight), 
            cornerRadius.BottomLeft, cornerRadius.BottomRight,
            cornerRadius.BottomRight, cornerRadius.BottomLeft);
        _transitions = transitions;
        _center = new Point(positionX, positionY);

        _maxRadius = Math.Sqrt(Math.Pow(outerWidth, 2) + Math.Pow(outerHeight, 2));
    }

    public override void OnRender(ImmediateDrawingContext drawingContext) {
        if (_lastServerTime.HasValue) _animationElapsed += (CompositionNow - _lastServerTime.Value);
        _lastServerTime = CompositionNow;

        var currentRadius = _maxRadius;
        var currentOpacity = _opacity;

        if (_transitions) {
            var expandingStep = _easing.Ease((double)_animationElapsed.Ticks / _duration.Ticks);
            currentRadius = _maxRadius * expandingStep;

            if (_secondStepStart is { } secondStepStart) {
                var opacityStep = _easing.Ease((double)(_animationElapsed - secondStepStart).Ticks /
                                               (_duration - secondStepStart).Ticks);
                currentOpacity = _opacity - _opacity * opacityStep;
            }
        }

        using (drawingContext.PushClip(_cornerRadiusRect)) {
            using (drawingContext.PushOpacity(currentOpacity, default)) {
                drawingContext.DrawEllipse(_brush, null, _center, currentRadius, currentRadius);
            }
        }
    }

    public override void OnMessage(object message) {
        if (message == FirstStepMessage) {
            _lastServerTime = null;
            _secondStepStart = null;
            RegisterForNextAnimationFrameUpdate();
        }
        else if (message == SecondStepMessage) {
            _secondStepStart = _animationElapsed;
        }
    }

    public override void OnAnimationFrameUpdate() {
        if (_animationElapsed >= _duration) return;
        Invalidate();
        RegisterForNextAnimationFrameUpdate();
    }
}