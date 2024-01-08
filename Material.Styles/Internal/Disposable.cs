using System;

namespace Material.Styles.Internal;

/// <summary>
/// Provides a set of static methods for creating <see cref="IDisposable"/> objects.
/// </summary>
internal static class Disposable {
    /// <summary>
    /// Gets the disposable that does nothing when disposed.
    /// </summary>
    public static IDisposable Empty => EmptyDisposable.Instance;

    /// <summary>
    /// Represents a disposable that does nothing on disposal.
    /// </summary>
    private sealed class EmptyDisposable : IDisposable {
        public static readonly EmptyDisposable Instance = new();

        private EmptyDisposable() { }

        public void Dispose() {
            // no op
        }
    }
}