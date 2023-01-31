using DDNSUpdater.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DDNSUpdater.Services;

public class TimerService : ITimerService
{
    private Timer timer;
    private readonly ILogger<TimerService> _logger;
    private readonly IServiceScopeFactory _factory;
    private readonly int intervalMinutes;

    public TimerService(ILogger<TimerService> logger,IServiceScopeFactory factory, IConfiguration configuration)
    {
        _logger = logger;
        _factory = factory;
        intervalMinutes = configuration.GetValue<int>("TimerIntervalMinutes");
        timer = new Timer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(intervalMinutes));
    }

    private async void TimerCallback(Object o)
    {
        _logger.LogDebug("Timer callback executed at " + DateTime.Now);
        await using var asyncScope = _factory.CreateAsyncScope();
        var ddnsService = asyncScope.ServiceProvider.GetRequiredService<DDNSService>();
        
        ddnsService.Update();
    }

    public void Start()
    {
        _logger.LogInformation("Timer service started.");
    }
}