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
    private readonly TimeSpan _fadeOutDuration;

    public RippleHandler(IImmutableBrush brush,
        Easing easing,
        TimeSpan duration,
        TimeSpan fadeOutDuration,
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
        _fadeOutDuration = fadeOutDuration;
    }

    public override void OnRender(ImmediateDrawingContext drawingContext) {
        OnUpdateTimerPrivate();

        double currentRadius;
        var currentOpacity = _opacity;
        var animationProgress = Math.Min((double)_animationElapsed.Ticks / _duration.Ticks, 1.0);
        var expandingStep = _easing.Ease(animationProgress);

        if (_transitions) {
            // Expansion always continues
            currentRadius = Math.Min(_maxRadius * expandingStep, _maxRadius);
            
            // Fade-out starts when the second message is received
            if (_secondStepStart is { } secondStepStart) {
                var timeSinceSecondStep = _animationElapsed - secondStepStart;
                var fadeOutProgress = Math.Min(Math.Max((double)timeSinceSecondStep.Ticks / _fadeOutDuration.Ticks, 0), 1);
                currentOpacity = _opacity * (1.0 - _easing.Ease(fadeOutProgress));
            }
        } else {
            currentRadius = _maxRadius;

            if (_secondStepStart != null)
                currentOpacity = 0.0;
        }

        using (drawingContext.PushClip(_cornerRadiusRect)) {
            using (drawingContext.PushOpacity(currentOpacity, default)) {
                drawingContext.DrawEllipse(_brush, null, _center, currentRadius, currentRadius);
            }
        }
    }

    private void OnUpdateTimerPrivate()
    {
        if (_lastServerTime.HasValue)
            _animationElapsed += CompositionNow - _lastServerTime.Value;

        _lastServerTime = CompositionNow;
    }

    public override void OnMessage(object message) {
        if (message == FirstStepMessage) {
            _lastServerTime = null;
            _animationElapsed = TimeSpan.Zero;
            _secondStepStart = null;
            TriggerNextFrameUpdatePrivate();
        }
        else if (message == SecondStepMessage) {
            // Record the time when the fade-out should begin
            OnUpdateTimerPrivate();
            _secondStepStart = _animationElapsed;
            TriggerNextFrameUpdatePrivate();
        }
    }

    public override void OnAnimationFrameUpdate() {
        // Continue animation until the expansion duration has elapsed OR the fade-out is complete
        if ((!_secondStepStart.HasValue && _animationElapsed < _duration) ||
            (_secondStepStart.HasValue && _animationElapsed < _secondStepStart + _fadeOutDuration))
        {
            TriggerNextFrameUpdatePrivate();
        }
    }

    private void TriggerNextFrameUpdatePrivate()
    {
        Invalidate();
        RegisterForNextAnimationFrameUpdate();
    }
}