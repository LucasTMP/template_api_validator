using Correlate.DependencyInjection;
using k8s.Models;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Template.Validator.Api.WebFlow.Filters;

namespace Template.Validator.Api.Config;

    public static class ConfigHttpClient
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("default")
                .AddPolicyHandler(GetPolicyRetry(configuration))
                .CorrelateRequests("X-Correlation-ID");
        }

        private static AsyncRetryPolicy<HttpResponseMessage> GetPolicyRetry(IConfiguration configuration)
        {
            var httpTentativas = configuration.GetValue<int>("Config:Polly:Attempts");
            var httpIntervalo = configuration.GetValue<int>("Config:Polly:Gap");

            return HttpPolicyExtensions.HandleTransientHttpError()
                       .WaitAndRetryAsync(httpTentativas,
                                          retryAttempt => TimeSpan.FromSeconds(
                                              Math.Pow(httpIntervalo,
                                                       retryAttempt)
                                          ),
                                          onRetry: (result, timeSpan, retryCount, context) =>
                                          {
                                              Serilog.Log.Error(
                                              $"Request failed with status code {result.Result?.StatusCode}. " +
                                              $"Uri: {result.Result?.RequestMessage.RequestUri.AbsoluteUri} " +
                                              $"Waiting {timeSpan} before next retry. Retry attempt {retryCount}"
                                              );
                                          });
        }

    }

