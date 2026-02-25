using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarService3.DL.Infrastructure
{
    internal class CarHostedService : IHostedService //BackgroundService
    {
        private readonly ILogger<CarHostedService> _logger;

        public CarHostedService(ILogger<CarHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"CarHostedService is running. {DateTime.Now}");

                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);
            
            return Task.CompletedTask;  
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
