using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using Template.Validator.Api.Web_Flow.Filters;
using Template.Validator.Api.Web_Flow.Middleware;
using Template.Validator.Infra.Data;

namespace Template.Validator.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers(config =>
        {
            config.Filters.Add(new ViewModelValidationFilter());
        });

        services.AddHealthChecks()
            .AddSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=TemplateValidator;",
                          name: "SQL Server Local",
                          tags: new[] { "DataBase" })
            .AddCheck<GoogleHealthCheck>("Google",
                                         failureStatus: HealthStatus.Unhealthy,
                                         tags: new[] { "Endpoint" })
            .AddCheck<GitHubAPIHealthCheck>("GitHub API",
                                            failureStatus: HealthStatus.Unhealthy,
                                            tags: new[] { "API" });

        services.AddHealthChecksUI(options =>
        {
            options.SetApiMaxActiveRequests(3);
            options.SetEvaluationTimeInSeconds(5);
            options.MaximumHistoryEntriesPerEndpoint(10);
            options.AddHealthCheckEndpoint("API With Health Checks", "/health");
        }).AddSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=TemplateValidator;");

        services.AddFluentValidation(options =>
        {
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=TemplateValidator;");
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.MigrationInitialisation();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            var path = "/dashboard";
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = path;
                options.AddCustomStylesheet("Resources/HealthChecksUIStyle.css");
            });
            endpoints.MapControllers();
        });
    }
}

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
            serviceScope.ServiceProvider.GetService<ApiDbContext>()?.Database.Migrate();
    }
}

public class GoogleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var ping = new Ping();
        PingReply pingStatus = ping.Send("www.google.com");

        if (pingStatus.Status == IPStatus.Success && pingStatus.RoundtripTime < 500)
            return Task.FromResult(HealthCheckResult.Healthy());

        if (pingStatus.RoundtripTime >= 500)
            return Task.FromResult(HealthCheckResult.Degraded());
        else
            return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed"));
    }
}

public class GitHubAPIHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET");

        var result = client.GetAsync("https://api.github.com/").Result;

        if (result.IsSuccessStatusCode)
            return Task.FromResult(HealthCheckResult.Healthy());
        else
            return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed"));
    }
}

