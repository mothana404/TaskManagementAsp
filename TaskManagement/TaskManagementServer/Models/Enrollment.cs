using TaskManagementServer.Enums;

namespace TaskManagementServer.Models;

public class Enrollment
{
    public int UserId { get; set; }
    public int TeamId { get; set; }

    public TeamRoles Role { get; set; } = TeamRoles.Member;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; }
    public Team Team { get; set; }
}
