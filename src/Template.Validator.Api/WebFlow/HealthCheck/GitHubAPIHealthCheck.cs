using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http.Headers;

namespace Template.Validator.Api.WebFlow.HealthCheck;

public class GitHubAPIHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET");

        var result = client.GetAsync("https://httpstat.us/200").Result;

        if (result.IsSuccessStatusCode)
            return Task.FromResult(HealthCheckResult.Healthy());
        else
            return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed"));
    }
}

