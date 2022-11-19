using Template.Validator.Api.Config;

namespace Template.Validator.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddConfigFluentValidation();
        services.AddConfigHealthChecks();
        services.AddConfigSwagger();
        services.AddConfigApp();
        services.AddConfigDbContext();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
