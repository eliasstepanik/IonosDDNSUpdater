using System;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Logging;
using DDNSUpdater.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

static T ParseEnum<T>(string value)
{
    return (T) Enum.Parse(typeof(T), value, true);
}

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

LogLevel loglevel;
if (Environment.GetEnvironmentVariables()["LogLevel"] != null)
    loglevel = ParseEnum<LogLevel>(Environment.GetEnvironmentVariables()["LogLevel"].ToString());
else
    loglevel = LogLevel.Information;


var configuration = builder.Build();

/*var logConfig = new OptionsMonitor<SpecterConsoleLoggerConfiguration>();
logConfig.CurrentValue.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
logConfig.CurrentValue.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;*/

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging(logging =>
    {
        
        logging.AddSpecterConsoleLogger(configuration =>
        {
            // Replace warning value from appsettings.json of "Cyan"
            configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
            configuration.LogLevelToColorMap[LogLevel.Debug] = ConsoleColor.DarkYellow;
            // Replace warning value from appsettings.json of "Red"
            configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
        });

        logging.SetMinimumLevel(loglevel);
    })
    .AddSingleton<ITimerService, TimerService>()
    .AddSingleton<DDNSService>()
    .BuildServiceProvider();




var dataAccess = serviceProvider.GetService<DDNSService>();
dataAccess.Start();

var timerService = serviceProvider.GetService<ITimerService>();
timerService.Start();

Console.ReadKey();
