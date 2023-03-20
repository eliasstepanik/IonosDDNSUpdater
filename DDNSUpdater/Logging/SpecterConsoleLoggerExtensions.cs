using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Spectre.Console;

namespace DDNSUpdater.Logging;

public static class SpecterConsoleLoggerExtensions
{
    public static ILoggingBuilder AddSpecterConsoleLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, SpecterConsoleLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <SpecterConsoleLoggerConfiguration, SpecterConsoleLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddSpecterConsoleLogger(
        this ILoggingBuilder builder,
        Action<SpecterConsoleLoggerConfiguration> configure)
    {
        builder.AddSpecterConsoleLogger();
        builder.Services.Configure(configure);

        return builder;
    }

    public static void LogTable(this ILogger logger, LogLevel logLevel, EventId eventId, Exception? exception, Table? table)
    {

        logger.Log(logLevel, eventId, table, exception, null);
    }
    
}