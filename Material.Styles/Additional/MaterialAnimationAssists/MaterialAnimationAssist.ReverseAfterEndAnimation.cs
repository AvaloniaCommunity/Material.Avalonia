using System.Threading;
using Avalonia;
using Avalonia.Animation;

namespace Material.Styles.Additional.MaterialAnimationAssists;

public static partial class MaterialAnimationAssist {
    public static readonly AttachedProperty<Animation> ReverseAfterEndAnimationProperty =
        AvaloniaProperty.RegisterAttached<Animatable, Animation>("ReverseAfterEndAnimation", typeof(MaterialAnimationAssist));

    public static Animation GetReverseAfterEndAnimation(Animatable element) {
        return element.GetValue(ReverseAfterEndAnimationProperty);
    }

    public static void SetReverseAfterEndAnimation(Animatable element, Animation value) {
        element.SetValue(ReverseAfterEndAnimationProperty, value);
    }

    private static void OnReverseAfterEndAnimationChanged(Animatable control, AvaloniaPropertyChangedEventArgs<Animation> args) {
        var oldData = GetAnimationInternalData<ReverseAfterEndAnimationData>(control, nameof(ReverseAfterEndAnimationProperty));
        oldData?.CancellationTokenSource.Cancel();

        var animation = args.GetNewValue<Animation?>();
        if (animation != null) {
            // Running animation
            var cancellationTokenSource = new CancellationTokenSource();
            _ = animation.RunAsync(control, cancellationTokenSource.Token);

            // Creating reversed animation
            var reversedAnimation = new Animation() {
                Delay = animation.Delay, Duration = animation.Duration, Easing = animation.Easing, FillMode = animation.FillMode,
                IterationCount = new IterationCount(1), SpeedRatio = animation.SpeedRatio
            };
            reversedAnimation.Children.AddRange(animation.Children);
            reversedAnimation.PlaybackDirection = animation.PlaybackDirection switch {
                PlaybackDirection.Normal           => PlaybackDirection.Reverse,
                PlaybackDirection.Reverse          => PlaybackDirection.Normal,
                PlaybackDirection.Alternate        => PlaybackDirection.AlternateReverse,
                PlaybackDirection.AlternateReverse => PlaybackDirection.Alternate,
                _                                  => PlaybackDirection.Reverse
            };

            var data = new ReverseAfterEndAnimationData(cancellationTokenSource, reversedAnimation);
            SetAnimationsInternalData(control, nameof(ReverseAfterEndAnimationProperty), data);
        }
        else if (oldData?.ReversedAnimation is not null) {
            var cancellationTokenSource = new CancellationTokenSource();
            _ = oldData.ReversedAnimation.RunAsync(control, cancellationTokenSource.Token);

            var data = new ReverseAfterEndAnimationData(cancellationTokenSource, null);
            SetAnimationsInternalData(control, nameof(ReverseAfterEndAnimationProperty), data);
        }
    }

    private sealed record ReverseAfterEndAnimationData(CancellationTokenSource CancellationTokenSource, Animation? ReversedAnimation) { }
}
