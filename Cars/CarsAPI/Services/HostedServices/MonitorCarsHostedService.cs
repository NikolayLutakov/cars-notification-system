using CarsAPI.Services.Interfaces;
using Cronos;

namespace CarsAPI.Services.HostedServices
{
    public class MonitorCarsHostedService : IHostedService
    {
        private const string cron = "* * * * *";
        private readonly CronExpression cronExpression;
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;

        public MonitorCarsHostedService(ILogger<MonitorCarsHostedService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.cronExpression = CronExpression.Parse(cron);

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.StartMonitoringCars(cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task StartMonitoringCars(CancellationToken cancellationToken)
        {
            var scope = this.serviceProvider.CreateScope();
            var carsService = scope.ServiceProvider.GetRequiredService<ICarsService>();

            while (!cancellationToken.IsCancellationRequested) 
            {
                try
                {
                    carsService.CheckCarsTaxesAsync();
                    this.logger.LogInformation("Cars check started.");
                }
                catch (Exception ex) 
                {
                    this.logger.LogError(ex.Message);
                    this.logger.LogError(ex.StackTrace);
                }

                await Task.Delay(this.CalculateDelay(), cancellationToken);
            }
        }

        private TimeSpan CalculateDelay()
        {
            var now = DateTime.UtcNow;
            var nextExecution = cronExpression.GetNextOccurrence(now).Value;
            var delay = nextExecution - now;

            return delay;
        }
    }
}
