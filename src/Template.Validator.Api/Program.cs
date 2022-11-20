using Serilog;
using Template.Validator.Api;
using Template.Validator.Api.Config;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.UseConfigBuilder();

    var startup = new Startup(builder.Configuration);
    startup.ConfigureServices(builder.Services);

    var app = builder.Build();
    startup.Configure(app, app.Environment);

    Log.Information("Starting app.");
    app.Run();   
}
catch (Exception ex)
{
    Log.Fatal(ex, "Fatal Error app.");
    throw;
}
finally
{
    Log.Information("App shutting down.");
    Log.CloseAndFlush();
}