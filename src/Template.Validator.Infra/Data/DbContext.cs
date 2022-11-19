using Microsoft.EntityFrameworkCore;
using Template.Validator.Domain.Fake;
using Template.Validator.Domain.Models;

namespace Template.Validator.Infra.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Aeroplane> Aeroplanes { get; set; }

    public ApiDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(e => e.GetProperties()
                 .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(50)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.Entity<Client>().HasData(new ClientFaker().Generate(10));
        modelBuilder.Entity<Aeroplane>().HasData(new AeroplaneFaker().Generate(10));

        base.OnModelCreating(modelBuilder);
    }
}


