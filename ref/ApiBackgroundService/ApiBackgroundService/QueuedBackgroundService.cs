
namespace ApiBackgroundService
{
    public class QueuedBackgroundService : BackgroundService
    {
        private readonly ILogger<QueuedBackgroundService> _logger;

        public QueuedBackgroundService(
            IBackgroundQueue queue,
            ILogger<QueuedBackgroundService> logger)
        {
            Queue = queue;
            _logger = logger;
        }

        public IBackgroundQueue Queue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"BackgroundService running...");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await Queue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }
    }
}
