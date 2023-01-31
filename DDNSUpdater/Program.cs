using DDNSUpdater.Interfaces;
using DDNSUpdater.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestSharp;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

var configuration = builder.Build();


var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddLogging(logging => logging.AddConsole())
    .AddSingleton<ITimerService, TimerService>()
    .AddSingleton<DDNSService>()
    .BuildServiceProvider();

var dataAccess = serviceProvider.GetService<DDNSService>();
dataAccess.Start();

var timerService = serviceProvider.GetService<ITimerService>();
timerService.Start();

Console.ReadKey();
