using System;
using DDNSUpdater;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Logging;
using DDNSUpdater.Services;
using Docker.DotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

DockerClient dockerClient = new DockerClientConfiguration()
    .CreateClient();

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
    .AddSingleton(dockerClient)
    .AddSingleton<DockerService>()
    .AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "DataContext"))
    .BuildServiceProvider();



var dockerService = serviceProvider.GetService<DockerService>();
dockerService?.UpdateDomainList();

var dataContext = serviceProvider.GetService<DataContext>();
var FoundDomains = dataContext.Domains.ToListAsync();

var dataAccess = serviceProvider.GetService<DDNSService>();
dataAccess?.Init();

var timerService = serviceProvider.GetService<ITimerService>();
timerService?.Start();

Console.ReadKey();
