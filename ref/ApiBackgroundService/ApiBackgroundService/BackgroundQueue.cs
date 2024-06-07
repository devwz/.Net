using System.Collections.Concurrent;

namespace ApiBackgroundService
{
    public interface IBackgroundQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }

    public class BackgroundQueue : IBackgroundQueue
    {
        ConcurrentQueue<Func<CancellationToken, Task>> _workItem = new ConcurrentQueue<Func<CancellationToken, Task>>();
        SemaphoreSlim _signal = new SemaphoreSlim(0);

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItem.TryDequeue(out var workItem);

            return workItem;
        }

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItem.Enqueue(workItem);
            _signal.Release();
        }
    }
}
