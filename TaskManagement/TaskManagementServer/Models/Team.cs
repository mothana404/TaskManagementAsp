namespace TaskManagementServer.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int TeamManagerId { get; set; }
    public User User { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<TaskItem> TaskItems { get; set; }
    public ICollection<TeamRequest> TeamRequests { get; set; }
}
