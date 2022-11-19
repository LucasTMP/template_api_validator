using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace Template.Validator.Api.WebFlow.HealthCheck
{
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
}
