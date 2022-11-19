using Template.Validator.Api.WebFlow.Filters;
using Template.Validator.Api.WebFlow.Middleware;

namespace Template.Validator.Api.Config
{
    public static class ConfigApp
    {
        public static void AddConfigApp(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddControllers(config =>
            {
                config.Filters.Add(new ViewModelValidationFilter());
            });
        }

        public static void UseConfigApp(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
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
