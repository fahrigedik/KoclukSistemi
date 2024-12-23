using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;
public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.GoalTitle).IsRequired().HasMaxLength(128);
        builder.Property(x => x.GoalDescription).IsRequired().HasMaxLength(512);
        builder.Property(x => x.GoalTypeId).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(x => x.GoalType)
            .WithMany(x => x.Goals)
            .HasForeignKey(x => x.GoalTypeId);

    }
}
