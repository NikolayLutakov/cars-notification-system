using CarsAPI.Constants;
using CarsAPI.Services.Interfaces;
using Cronos;

namespace CarsAPI.Services.HostedServices
{
    public class MonitorCarsHostedService : IHostedService
    {  
        private readonly CronExpression cronExpression;
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;

        private DateTime nextExecution;

        public MonitorCarsHostedService(ILogger<MonitorCarsHostedService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;

            var cron = Environment.GetEnvironmentVariable("MONITOR_CRON") ?? CarsMonitorConstants.DefaultCron;

            this.cronExpression = CronExpression.Parse(cron);

            this.nextExecution = cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;
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
                    if(DateTime.UtcNow >= nextExecution)
                    {
                        this.logger.LogInformation("Cars check starting.");

                        await carsService.CheckCarsTaxesAsync();

                        this.logger.LogInformation("Cars check finished.");

                        this.nextExecution = this.cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;

                        this.logger.LogInformation($"Next execution scheduled for: {this.nextExecution}");
                    }
                    
                }
                catch (Exception ex) 
                {
                    this.nextExecution = this.cronExpression.GetNextOccurrence(DateTime.UtcNow).Value;

                    this.logger.LogError(ex.Message);
                    this.logger.LogError(ex.StackTrace);
                }

                var delay = this.CalculateTimeUntilNextHour(DateTime.UtcNow);

                this.logger.LogInformation($"Next iteration delay: {delay}");

                await Task.Delay(delay, cancellationToken);
            }
        }

        private TimeSpan CalculateTimeUntilNextHour(DateTime currentTime)
        {
            var nextHour = currentTime.AddHours(1).Date.AddHours(currentTime.Hour + 1);

            var timeUntilNextHour = nextHour - currentTime;

            return timeUntilNextHour;
        }
    }
}
