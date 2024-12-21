using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.AuthServer.Core.Entities;

namespace MS.AuthServer.Data.Configuration;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(128);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(128);
    }
}
