using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementServer.Enums;
using TaskManagementServer.Models;

namespace TaskManagementServer.Data.Config;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoleName)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(r => r.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(u => u.RoleId);

        builder.HasData(
            new Role { Id = 1, RoleName = UserRoles.Member },
            new Role { Id = 2, RoleName = UserRoles.Manager },
            new Role { Id = 3, RoleName = UserRoles.Admin }
        );
    }
}
