using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Models;

namespace TaskManagementServer.Data.Config;

public class TeamRequestConfig : IEntityTypeConfiguration<TeamRequest>
{
    public void Configure(EntityTypeBuilder<TeamRequest> builder)
    {
        builder.HasKey(tr => tr.Id);

        builder.Property(tr => tr.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(tr => tr.User)
            .WithMany(u => u.TeamRequests)
            .HasForeignKey(tr => tr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tr => tr.Team)
            .WithMany(t => t.TeamRequests)
            .HasForeignKey(tr => tr.TeamId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
