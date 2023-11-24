using CarsAPI.Constants;
using CarsAPI.Services.Interfaces;

namespace CarsAPI.Services.HostedServices
{
    public class MonitorCarsHostedService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;
        private readonly int executionHour;

        public MonitorCarsHostedService(ILogger<MonitorCarsHostedService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;

            var envHour = Environment.GetEnvironmentVariable("EXECUTION_HOUR");

            executionHour = envHour != null ? int.Parse(envHour) : CarsMonitorConstants.DefaultExecutionTime;
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

            var firstRun = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                if (!firstRun)
                {
                    try
                    {
                        this.logger.LogInformation("Cars check starting.");

                        await carsService.CheckCarsTaxesAsync();

                        this.logger.LogInformation("Cars check finished.");

                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex.Message);
                        this.logger.LogError(ex.StackTrace);
                    }
                }

                var delay = this.CalculateDelay();

                this.logger.LogInformation($"Next execution scheduled for: {DateTime.UtcNow.AddMilliseconds(delay)}");
                this.logger.LogInformation($"Next iteration delay: {TimeSpan.FromMilliseconds(delay)}");

                firstRun = false;

                await Task.Delay(delay, cancellationToken);
            }
        }

        private int CalculateDelay()
        {
            var now = DateTime.UtcNow;

            var nextExecutionDate = DateTime.UtcNow.AddDays(1).Date;

            var nextExecutionDateTime = nextExecutionDate.AddHours(executionHour);

            var delay = (int)(nextExecutionDateTime - now).TotalMilliseconds;

            return delay;
        }
    }
}
