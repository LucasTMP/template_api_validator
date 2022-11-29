using Template.Validator.Api.Config;

namespace Template.Validator.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConfigFluentValidation();
        services.AddConfigHealthChecks();
        services.AddHttpClients(Configuration);
        services.AddDependencyInjection();
        services.AddConfigSwagger();
        services.AddConfigApp();
        services.AddConfigDbContext();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseConfigDbContext();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseConfigApp();
    }
}