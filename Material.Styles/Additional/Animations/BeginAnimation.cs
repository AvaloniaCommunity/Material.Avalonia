using System.Reactive.Subjects;

namespace MaterialXamlToolKit.Avalonia.Additional.Animations {
    /// <summary>
    /// Tracks the progress of an animation. Will always continue to end or until restart.
    /// Perhaps this has already been implemented without such crutches. Issue: https://github.com/AvaloniaUI/Avalonia/issues/4673
    /// </summary>
    public class BeginAnimation : ControllableAnimationBase {
        internal override void OnNext(Subject<bool> match, bool previous, bool obj) {
            if (obj) return;
            // "Turning" off
            match.OnNext(false);
            
            // Then "turning" on
            match.OnNext(true);
        }
    }
}