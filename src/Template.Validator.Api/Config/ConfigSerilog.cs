using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;

namespace Template.Validator.Api.Config;

public static class ConfigSerilog
{
    public static void AddSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}

public static class LogEnricher
{
    public static void AddConfig(this RequestLoggingOptions requestLoggingOptions)
    {
        requestLoggingOptions.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;
    }
}