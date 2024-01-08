using System;

namespace Material.Styles.Internal;

internal class LightweightSubject<T> : LightweightObservableBase<T> {
    public void OnCompleted() {
        PublishCompleted();
    }

    public void OnError(Exception error) {
        PublishError(error);
    }

    public void OnNext(T value) {
        PublishNext(value);
    }

    protected override void Initialize() { }

    protected override void Deinitialize() { }
}