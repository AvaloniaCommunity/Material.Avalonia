using System.Collections.Generic;
using Avalonia;
using Avalonia.Animation;

namespace Material.Styles.Additional.MaterialAnimationAssists;

public static partial class MaterialAnimationAssist {
    private static readonly AttachedProperty<Dictionary<string, object>?> AnimationsInternalDataProperty =
        AvaloniaProperty.RegisterAttached<Animatable, Dictionary<string, object>?>("AnimationsInternalData", typeof(MaterialAnimationAssist));
    static MaterialAnimationAssist() {
        ContinuousAnimationProperty.Changed.AddClassHandler<Animatable, Animation>(OnBeginAnimationChanged);
        ReverseAfterEndAnimationProperty.Changed.AddClassHandler<Animatable, Animation>(OnReverseAfterEndAnimationChanged);
    }

    private static void SetAnimationsInternalData(Animatable element, string key, object value) {
        var internalDataDictionary = element.GetValue(AnimationsInternalDataProperty);
        if (internalDataDictionary == null) {
            internalDataDictionary = new Dictionary<string, object>();
            element.SetValue(AnimationsInternalDataProperty, internalDataDictionary);
        }

        internalDataDictionary[key] = value;
    }

    private static T? GetAnimationInternalData<T>(Animatable element, string key) where T : class {
        var animationsInternalData = element.GetValue(AnimationsInternalDataProperty);
        return animationsInternalData?.TryGetValue(key, out var value) == true ? (T)value : null;
    }
}
