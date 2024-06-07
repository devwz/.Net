
namespace ApiBackgroundService
{
    public class BackgroundScopedService : BackgroundService
    {
        private readonly ILogger<BackgroundScopedService> _logger;

        public BackgroundScopedService(ILogger<BackgroundScopedService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => _logger.LogInformation("Working..."), stoppingToken);
        }

        public async Task Run(CancellationToken stoppingToken)
        {
            for (int i = 0; i < 10; i++)
            {
                _logger.LogInformation($"Running... {i}");

                await Task.Delay(4096, stoppingToken);
            }
        }
    }
}
