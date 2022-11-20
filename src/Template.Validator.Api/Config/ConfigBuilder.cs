using Serilog;

namespace Template.Validator.Api.Config;

public static class ConfigBuilder
{
    public static void UseConfigBuilder(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        ConfigSerilog.AddSerilog(builder.Configuration);
        builder.Host.UseSerilog(Log.Logger);
    }
}