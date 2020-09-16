using System;
using System.Reactive.Subjects;
using Avalonia.Animation;

namespace MaterialXamlToolKit.Avalonia.Additional.Animations {
    public class ReverseAfterEndAnimation : ControllableAnimationBase {

        public override IDisposable Apply(Animatable control, IClock clock, IObservable<bool> match, Action onComplete = null) {
            var reversedAnimation = new Animation() {
                Delay = Animation.Delay, Duration = Animation.Duration, Easing = Animation.Easing, FillMode = Animation.FillMode,
                IterationCount = new IterationCount(1), SpeedRatio = Animation.SpeedRatio
            };
            reversedAnimation.Children.AddRange(Animation.Children);
            reversedAnimation.PlaybackDirection = Animation.PlaybackDirection switch {
                PlaybackDirection.Normal           => PlaybackDirection.Reverse,
                PlaybackDirection.Reverse          => PlaybackDirection.Normal,
                PlaybackDirection.Alternate        => PlaybackDirection.AlternateReverse,
                PlaybackDirection.AlternateReverse => PlaybackDirection.Alternate,
                _                                  => PlaybackDirection.Reverse
            };
            
            // Applying reversed animation
            var reversedObserver = new Subject<bool>();
            match.Subscribe(b => reversedObserver.OnNext(!b));
            reversedAnimation.Apply(control, clock, reversedObserver, () => {});
            
            return base.Apply(control, clock, match, onComplete);
        }
    }
}