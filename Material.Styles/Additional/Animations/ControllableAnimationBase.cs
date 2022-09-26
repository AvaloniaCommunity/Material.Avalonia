using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Metadata;

namespace Material.Styles.Additional.Animations
{
    /// <summary>
    ///     Tracks the progress of an animation. This class should act the same as <see cref="Animation" />
    /// </summary>
    public class ControllableAnimationBase : AvaloniaObject, IAnimation
    {
        public static readonly DirectProperty<ControllableAnimationBase, Animation> AnimationProperty =
            AvaloniaProperty.RegisterDirect<ControllableAnimationBase, Animation>(
                nameof(_animation),
                o => o._animation,
                (o, v) => o._animation = v);

        private Animation _animation;

        [Content]
        public Animation Animation
        {
            get => _animation;
            set => SetAndRaise(AnimationProperty, ref _animation, value);
        }

        public virtual IDisposable Apply(Animatable control, IClock clock, IObservable<bool> match,
            Action onComplete = null)
        {
            var previous = false;
            var observable = new Subject<bool>();
            match.Subscribe(b =>
            {
                OnNext(observable, previous, b);
                previous = b;
            });
            return Animation.Apply(control, clock, observable, onComplete);
        }

        public virtual Task RunAsync(Animatable control, IClock clock,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return Animation.RunAsync(control, clock, cancellationToken);
        }

        internal virtual void OnNext(Subject<bool> match, bool previous, bool obj)
        {
            match.OnNext(obj);
        }
    }
}