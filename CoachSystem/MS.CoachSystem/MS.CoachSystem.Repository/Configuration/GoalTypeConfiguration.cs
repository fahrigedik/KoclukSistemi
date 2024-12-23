using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;
public class GoalTypeConfiguration : IEntityTypeConfiguration<GoalType>
{
    public void Configure(EntityTypeBuilder<GoalType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TypeName).IsRequired();

        builder.HasMany(x => x.Goals)
            .WithOne(x => x.GoalType)
            .HasForeignKey(x => x.GoalTypeId);
    }
}
