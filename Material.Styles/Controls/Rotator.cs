using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

// ReSharper disable ConvertToLambdaExpression

namespace Material.Styles.Controls;

public class Rotator : Panel {
    public static readonly DirectProperty<Rotator, double> SpeedProperty =
        AvaloniaProperty.RegisterDirect(nameof(Speed),
            rotator => rotator._speed,
            (Rotator rotator, double v) => {
                if (rotator.IsEffectivelyVisible == false || rotator.IsEffectivelyEnabled == false)
                    return;

                rotator._speed = v;
                rotator.RequestAnimationIfNeeded();
            });

    private readonly RotateTransform _renderTransform;
    private bool _isAnimationActionRunning;
    private TimeSpan _lastRenderTime = TimeSpan.Zero;
    private double _rotateDegree;
    private double _speed = 0.4;

    static Rotator() {
        IsEffectivelyEnabledProperty.Changed.AddClassHandler<Rotator>((o, _) => o.RequestAnimationIfNeeded());
    }

    public Rotator() {
        RenderTransform = _renderTransform = new RotateTransform();
    }

    public double Speed {
        get => _speed;
        set => SetAndRaise(SpeedProperty, ref _speed, value);
    }

    private void RequestAnimationIfNeeded() {
        if (_isAnimationActionRunning) return;
        _isAnimationActionRunning = true;
        TopLevel.GetTopLevel(this)?.RequestAnimationFrame(OnAnimationFrame);
    }

    private void OnAnimationFrame(TimeSpan renderTime) {
        if (IsEffectivelyVisible == false || IsEffectivelyEnabled == false || _speed == 0) {
            _isAnimationActionRunning = false;
            return;
        }

        var delta = renderTime - _lastRenderTime;
        _rotateDegree += _speed * delta.TotalMilliseconds;

        _lastRenderTime = renderTime;
        _rotateDegree %= 360;
        _renderTransform.Angle = _rotateDegree;

        TopLevel.GetTopLevel(this)?.RequestAnimationFrame(OnAnimationFrame);
    }

    /// <inheritdoc />
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnAttachedToVisualTree(e);
        RequestAnimationIfNeeded();
    }
}
