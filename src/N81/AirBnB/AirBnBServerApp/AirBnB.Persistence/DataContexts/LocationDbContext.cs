using AirBnB.DoMain.Entites;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.DataContexts;

public class LocationDbContext(DbContextOptions<LocationDbContext> options) : DbContext(options)
{
    public DbSet<Location> Locations => Set<Location>();

    public DbSet<LocationCategory> LocationCategories => Set<LocationCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var test = modelBuilder.Model.GetEntityTypes();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationDbContext).Assembly);
    }
}
