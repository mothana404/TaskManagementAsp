using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementServer.Models;

namespace TaskManagementServer.Data.Config;

public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => new { e.UserId, e.TeamId });

        builder.HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Team)
            .WithMany(t => t.Enrollments)
            .HasForeignKey(e => e.TeamId);
    }
}
