using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;
public class ResourceTagConfiguration : IEntityTypeConfiguration<ResourceTag>
{
    public void Configure(EntityTypeBuilder<ResourceTag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TagName).IsRequired();
    }
}

