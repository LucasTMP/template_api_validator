using Microsoft.EntityFrameworkCore;
using Serilog;
using Template.Validator.Infra.Data;

namespace Template.Validator.Api.Config
{
    public static class ConfigDbContext
    {
        public static void AddConfigDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=TemplateValidator;");
                options.EnableSensitiveDataLogging();
                options.LogTo(Log.Logger.Debug, LogLevel.Debug);
            });
        }

        public static void UseConfigDbContext(this IApplicationBuilder app)
        {
            app.MigrationInitialisation();
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
}
