
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository.Configuration;

public class CoachStudentConfiguration : IEntityTypeConfiguration<CoachStudent>
{
    public void Configure(EntityTypeBuilder<CoachStudent> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd(); 
        builder.Property(x => x.CoachId).IsRequired();
        builder.Property(x => x.StudentId);
    }
}

