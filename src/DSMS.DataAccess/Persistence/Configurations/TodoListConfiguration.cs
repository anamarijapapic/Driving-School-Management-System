using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DSMS.Core.Entities;

namespace DSMS.DataAccess.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.Property(tl => tl.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(tl => tl.Items)
            .WithOne(ti => ti.List)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
