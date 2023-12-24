using AirBnB.DoMain.Entites;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class LocationConfiguration(LocationDbContext dbContext) : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.Property(location => location.ImageUrl).IsRequired();
        builder.Property(location => location.Name).IsRequired();
    }
}
