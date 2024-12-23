using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;

public class ResourceToTagConfiguration : IEntityTypeConfiguration<ResourceToTag>
{
    public void Configure(EntityTypeBuilder<ResourceToTag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ResourceID).IsRequired();
        builder.Property(x => x.ResourceTagID).IsRequired();

        builder.HasOne(x => x.Resource)
            .WithMany(x => x.ResourceToTags)
            .HasForeignKey(x => x.ResourceID);

        builder.HasOne(x => x.ResourceTag)
            .WithMany(x => x.ResourceToTags)
            .HasForeignKey(x => x.ResourceTagID);
    }
}
