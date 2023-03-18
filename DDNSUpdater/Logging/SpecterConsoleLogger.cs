using DDNSUpdater.Interfaces;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Console = Spectre.Console.AnsiConsole;

namespace DDNSUpdater.Logging;

public sealed class SpecterConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly Func<SpecterConsoleLoggerConfiguration> _getCurrentConfig;
    public SpecterConsoleLogger(
        string name, Func<SpecterConsoleLoggerConfiguration> getCurrentConfig) =>
        (_name, _getCurrentConfig) = (name, getCurrentConfig);
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        Table table = null;
        try
        {
            table = state as Table;
            
                
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
        

        
        SpecterConsoleLoggerConfiguration config = _getCurrentConfig();
        if (table is not null)
        {
            Console.Write(table);
        }
        else if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            var originalColor = new Style(foreground: System.Console.ForegroundColor);
            
            
            Console.Foreground = config.LogLevelToColorMap[logLevel];
            Console.Write($"[{eventId.Id,2}:{logLevel,-12}]");
            
            Console.Foreground = originalColor.Foreground;
            Console.Write($"     {_name} - ");

            Console.Foreground = config.LogLevelToColorMap[logLevel];
            Console.Write($"{formatter(state, exception)}");
            
            Console.Foreground = originalColor.Foreground;
            Console.WriteLine();
        }
    }
    
    

    public bool IsEnabled(LogLevel logLevel) =>
        _getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;
}