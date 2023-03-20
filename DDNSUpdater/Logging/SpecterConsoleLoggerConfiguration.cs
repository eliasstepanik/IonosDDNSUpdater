using Microsoft.Extensions.Logging;

namespace DDNSUpdater.Logging;

public sealed class SpecterConsoleLoggerConfiguration
{
    public int EventId { get; set; }

    public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
    {
        [LogLevel.Information] = ConsoleColor.DarkGreen
    };
}