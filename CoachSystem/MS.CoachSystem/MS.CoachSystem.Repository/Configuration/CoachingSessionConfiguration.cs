using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;
public class CoachingSessionConfiguration : IEntityTypeConfiguration<CoachingSession>
{
    public void Configure(EntityTypeBuilder<CoachingSession> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CoachId).IsRequired();
        builder.Property(x => x.StudentId).IsRequired();

        builder.Property(x => x.SessionTopic).HasMaxLength(128);
        builder.Property(x => x.SessionNotes).HasMaxLength(512);
        builder.Property(x => x.SessionDate).IsRequired();
    }
}
