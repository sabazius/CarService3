using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService3.DL.Infrastructure
{
    internal class CarBackgroundService : BackgroundService
    {
        private readonly ILogger<CarHostedService> _logger;

        public CarBackgroundService(ILogger<CarHostedService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"CarBackgroundService is running. {DateTime.Now}");
                // Your background task logic here
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
