using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace DDNSUpdater.Logging;

[UnsupportedOSPlatform("browser")]
[ProviderAlias("SpecterConsole")]
public sealed class SpecterConsoleLoggerProvider : ILoggerProvider
{
    private readonly IDisposable? _onChangeToken;
    private SpecterConsoleLoggerConfiguration _currentConfig;
    private readonly ConcurrentDictionary<string, SpecterConsoleLogger> _loggers =
        new(StringComparer.OrdinalIgnoreCase);
    public SpecterConsoleLoggerProvider(
        IOptionsMonitor<SpecterConsoleLoggerConfiguration> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    }
    
    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new SpecterConsoleLogger(name, GetCurrentConfig));

    private SpecterConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken?.Dispose();
    }
}