using System;
using System.Threading;
using DDNSUpdater.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DDNSUpdater.Services
{
    public class TimerService : ITimerService
    {
        private Timer timer;
        private readonly ILogger<TimerService> _logger;
        private readonly IServiceScopeFactory _factory;
        private readonly int intervalMinutes;

        public TimerService(ILogger<TimerService> logger, IServiceScopeFactory factory, IConfiguration configuration)
        {
            _logger = logger;
            _factory = factory;

            // Read the interval time for the timer from the appsettings.json file
            intervalMinutes = configuration.GetValue<int>("TimerIntervalMinutes");

            // Create a new Timer object that executes the TimerCallback method at intervals specified by intervalMinutes
            timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(intervalMinutes));
        }

        // This method is called each time the timer ticks
        private async void TimerCallback(object o)
        {
            _logger.LogDebug("Timer callback executed at " + DateTime.Now);

            // Create a new service scope using the IServiceScopeFactory
            await using var asyncScope = _factory.CreateAsyncScope();

            // Retrieve an instance of the DDNSService from the service scope and call its Update method to perform the DDNS update
            var ddnsService = asyncScope.ServiceProvider.GetRequiredService<DDNSService>();
            ddnsService.Update();
        }

        // This method is called after the timer is initialized
        public void Start()
        {
            _logger.LogInformation("Timer service started.");
        }
    }
}