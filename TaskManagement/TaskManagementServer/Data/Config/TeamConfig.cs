using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Models;

namespace TaskManagementServer.Data.Config;

public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.TeamManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Enrollments)
            .WithOne(e => e.Team)
            .HasForeignKey(e => e.TeamId);

        builder.HasMany(t => t.TaskItems)
            .WithOne(task => task.Team)
            .HasForeignKey(task => task.TeamId);
    }
}
