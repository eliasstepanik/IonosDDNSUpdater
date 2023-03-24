using System;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Logging;
using DDNSUpdater.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

// A generic method to parse a string value into an enum of type T
static T ParseEnum<T>(string value)
{
    return (T) Enum.Parse(typeof(T), value, true);
}

// Build the configuration object by loading configuration settings from an appsettings.json file
var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

// Read the log level from the environment variable LogLevel, or set it to LogLevel.Information if the variable is not set
LogLevel loglevel;
if (Environment.GetEnvironmentVariables()["LogLevel"] != null)
    loglevel = ParseEnum<LogLevel>(Environment.GetEnvironmentVariables()["LogLevel"].ToString());
else
    loglevel = LogLevel.Information;

var configuration = builder.Build();

// Set up the service collection to configure and register application services
var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging(logging =>
    {
        // Add a custom SpecterConsoleLogger to the logging pipeline with color mappings for different log levels
        logging.AddSpecterConsoleLogger(configuration =>
        {
            configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.DarkCyan;
            configuration.LogLevelToColorMap[LogLevel.Debug] = ConsoleColor.DarkYellow;
            configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
        });

        // Set the minimum log level to the value of the loglevel variable
        logging.SetMinimumLevel(loglevel);
    })
    .AddSingleton<ITimerService, TimerService>()
    .AddSingleton<DDNSService>()
    .BuildServiceProvider();

// Retrieve the DDNSService and ITimerService from the service provider and start them
var dataAccess = serviceProvider.GetService<DDNSService>();
dataAccess.Start();

var timerService = serviceProvider.GetService<ITimerService>();
timerService.Start();

// Wait for a key press before exiting the program
Console.ReadKey();
