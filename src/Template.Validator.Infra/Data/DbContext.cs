using Microsoft.EntityFrameworkCore;
using Template.Validator.Domain.Fake;
using Template.Validator.Domain.Models;

namespace Template.Validator.Infra.Data;

public class ApiDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Aeroplane> aeroplanes { get; set; }

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
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        foreach(var i in Enumerable.Range(1, 200))
        {
            modelBuilder.Entity<Client>().HasData(new ClientFaker());
            modelBuilder.Entity<Aeroplane>().HasData(new AeroplaneFaker());
        }

        base.OnModelCreating(modelBuilder);

        
    }
}


