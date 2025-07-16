using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Material.Dialog.Collections;

internal class BlockingConcurrentQueue<T>
{
    private readonly ConcurrentQueue<T> _queue = new();
    private readonly SemaphoreSlim _signal = new(0);

    public void Enqueue(T item)
    {
        _queue.Enqueue(item);
        _signal.Release(); // Signal that an item is available
    }

    public async Task<T> DequeueAsync(CancellationToken cancellationToken = default)
    {
        await _signal.WaitAsync(cancellationToken); // Wait until an item is available
        _queue.TryDequeue(out T item); // Dequeue is guaranteed to succeed after WaitAsync
        return item;
    }

    public bool TryDequeue(out T item)
    {
        return _queue.TryDequeue(out item);
    }

    public int Count => _queue.Count;
}