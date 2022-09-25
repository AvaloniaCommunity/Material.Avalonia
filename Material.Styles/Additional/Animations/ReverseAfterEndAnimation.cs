using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Avalonia.Animation;

namespace Material.Styles.Additional.Animations
{
    public class ReverseAfterEndAnimation : ControllableAnimationBase
    {
        public bool WaitTillEnd { get; set; } = false;

        // Required WaitTillEnd == true
        public TimeSpan DelayBetweenReverse { get; set; } = TimeSpan.Zero;

        public override IDisposable Apply(Animatable control, IClock clock, IObservable<bool> match,
            Action onComplete = null)
        {
            var reversedAnimation = new Animation
            {
                Delay = Animation.Delay, Duration = Animation.Duration, Easing = Animation.Easing,
                FillMode = Animation.FillMode,
                IterationCount = new IterationCount(1), SpeedRatio = Animation.SpeedRatio
            };
            reversedAnimation.Children.AddRange(Animation.Children);
            reversedAnimation.PlaybackDirection = Animation.PlaybackDirection switch
            {
                PlaybackDirection.Normal => PlaybackDirection.Reverse,
                PlaybackDirection.Reverse => PlaybackDirection.Normal,
                PlaybackDirection.Alternate => PlaybackDirection.AlternateReverse,
                PlaybackDirection.AlternateReverse => PlaybackDirection.Alternate,
                _ => PlaybackDirection.Reverse
            };

            var lastValue = false;
            // Applying reversed animation
            var reversedObserver = new Subject<bool>();
            var timeOut = Task.CompletedTask;

            match.Subscribe(async b =>
            {
                if (lastValue == b) return;
                lastValue = b;
                if (b)
                {
                    reversedObserver.OnNext(false);
                    timeOut = Task.Delay(reversedAnimation.Duration + DelayBetweenReverse);
                }
                else
                {
                    await timeOut;
                    reversedObserver.OnNext(true);
                }
            });
            reversedAnimation.Apply(control, clock, reversedObserver, () => { });

            return base.Apply(control, clock, match, onComplete);
        }

        internal override void OnNext(Subject<bool> match, bool previous, bool obj)
        {
            if (WaitTillEnd)
            {
                if (obj) return;
                // "Turning" off
                match.OnNext(false);

                // Then "turning" on
                match.OnNext(true);
            }
            else
            {
                base.OnNext(match, previous, obj);
            }
        }
    }
}