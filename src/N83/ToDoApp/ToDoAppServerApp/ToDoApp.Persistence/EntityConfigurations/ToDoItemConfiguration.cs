using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Persistence.EntityConfigurations;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.Property(todo => todo.Title).IsRequired().HasMaxLength(128);
    }
}
