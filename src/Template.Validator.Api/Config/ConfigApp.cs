using Correlate.AspNetCore;
using Correlate.DependencyInjection;
using Serilog;
using Serilog.Context;
using Template.Validator.Api.WebFlow.Filters;
using Template.Validator.Api.WebFlow.Middleware;

namespace Template.Validator.Api.Config
{
    public static class ConfigApp
    {
        public static void AddConfigApp(this IServiceCollection services)
        {
            services.AddCorrelate(options => options.RequestHeaders = new[]{"X-Correlation-ID"});
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(config =>
            {
                config.Filters.Add(new ViewModelValidationFilter());
            });
        }

        public static void UseConfigApp(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(opts => opts.AddConfig());
            app.UseCorrelate();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<RequestLogContextMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.AddConfigEndpointHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
