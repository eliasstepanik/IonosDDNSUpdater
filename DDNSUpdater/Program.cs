using System;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Logging;
using DDNSUpdater.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);



var configuration = builder.Build();

/*var logConfig = new OptionsMonitor<SpecterConsoleLoggerConfiguration>();
logConfig.CurrentValue.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
logConfig.CurrentValue.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;*/

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging(logging => logging.AddSpecterConsoleLogger(configuration =>
    {
        // Replace warning value from appsettings.json of "Cyan"
        configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
        // Replace warning value from appsettings.json of "Red"
        configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
    }))
    .AddSingleton<ITimerService, TimerService>()
    .AddSingleton<DDNSService>()
    .BuildServiceProvider();




var dataAccess = serviceProvider.GetService<DDNSService>();
dataAccess.Start();

var timerService = serviceProvider.GetService<ITimerService>();
timerService.Start();

Console.ReadKey();
