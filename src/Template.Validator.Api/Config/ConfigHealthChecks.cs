using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Template.Validator.Api.Web_Flow.HealthCheck;

namespace Template.Validator.Api.Config;

public static class ConfigHealthChecks
{
    public static void AddConfigHealthChecks(this IServiceCollection services)
    {
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
    }

    public static void AddConfigEndpointHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = p => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapHealthChecksUI(options =>
        {
            options.AddCustomStylesheet("Resources/HealthChecksUIStyle.css");
        });
    }
}

