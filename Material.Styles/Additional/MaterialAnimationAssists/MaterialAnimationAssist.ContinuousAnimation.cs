using System.Threading;
using Avalonia;
using Avalonia.Animation;

namespace Material.Styles.Additional.MaterialAnimationAssists;

public static partial class MaterialAnimationAssist {
    public static readonly AttachedProperty<Animation> ContinuousAnimationProperty =
        AvaloniaProperty.RegisterAttached<Animatable, Animation>("ContinuousAnimation", typeof(MaterialAnimationAssist));

    public static Animation GetContinuousAnimation(Animatable element) {
        return element.GetValue(ContinuousAnimationProperty);
    }

    public static void SetContinuousAnimation(Animatable element, Animation value) {
        element.SetValue(ContinuousAnimationProperty, value);
    }

    private static void OnBeginAnimationChanged(Animatable control, AvaloniaPropertyChangedEventArgs<Animation> args) {
        var animation = args.GetNewValue<Animation?>();
        if (animation != null) {
            // Cancelling the old one
            var cancellationTokenSource = GetAnimationInternalData<CancellationTokenSource>(control, nameof(ContinuousAnimationProperty));
            cancellationTokenSource?.Cancel();

            // Running a new one
            cancellationTokenSource = new CancellationTokenSource();
            SetAnimationsInternalData(control, nameof(ContinuousAnimationProperty), cancellationTokenSource);
            _ = animation.RunAsync(control, cancellationTokenSource.Token);
        }
    }
}
