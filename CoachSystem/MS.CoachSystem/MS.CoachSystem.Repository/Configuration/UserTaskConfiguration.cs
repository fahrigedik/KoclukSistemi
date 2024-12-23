using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;

public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StudentID).IsRequired();
        builder.Property(x => x.CoachID).IsRequired();
        builder.Property(x => x.Title).IsRequired();
    }
}
