using Microsoft.EntityFrameworkCore;
using Template.Validator.Infra.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(options => {
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=TemplateValidator;");
    options.EnableSensitiveDataLogging();
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrationInitialisation();

app.Run();



public static class DatabaseManagementService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<ApiDbContext>().Database.Migrate();
        }
    }
}