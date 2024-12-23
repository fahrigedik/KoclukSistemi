using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;
public class CoachingResourceConfiguration : IEntityTypeConfiguration<CoachingResource>
{
    public void Configure(EntityTypeBuilder<CoachingResource> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CoachID).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(512);
        builder.Property(x => x.URL).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();

    }
}
