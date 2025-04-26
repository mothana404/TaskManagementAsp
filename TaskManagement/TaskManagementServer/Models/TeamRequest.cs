using TaskManagementServer.Enums;

namespace TaskManagementServer.Models;

public class TeamRequest
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int TeamId { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    public User User { get; set; }
    public Team Team { get; set; }
}
