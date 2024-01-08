using System;
using Avalonia.Reactive;

namespace Material.Styles.Internal;

internal static class Observable {
    public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> action) {
        return source.Subscribe(new AnonymousObserver<T>(action));
    }
}