using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;

public class ResourceTypeConfiguration : IEntityTypeConfiguration<ResourceType>
{
    public void Configure(EntityTypeBuilder<ResourceType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TypeName).IsRequired();

        builder.HasMany(x => x.CoachingResources)
            .WithOne(x => x.ResourceTypeNavigation)
            .HasForeignKey(x => x.ResourceTypeId);
    }
}
