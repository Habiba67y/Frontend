using AirBnB.DoMain.Entites;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnB.Persistence.EntityConfigurations;

public class LocationCategoryConfiguration(LocationDbContext dbContext) : IEntityTypeConfiguration<LocationCategory>
{
    public void Configure(EntityTypeBuilder<LocationCategory> builder)
    {
        builder.Property(locationCategory => locationCategory.ImageUrl).IsRequired();
        builder.Property(locationCategory => locationCategory.Name).IsRequired();
    }
}
