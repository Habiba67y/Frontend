using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Persistence.DataContexts;

public class AppDbContext : DbContext
{
    DbSet<ToDoItem> ToDos => Set<ToDoItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
