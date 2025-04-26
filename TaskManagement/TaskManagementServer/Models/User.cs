namespace TaskManagementServer.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password{ get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<TeamRequest> TeamRequests { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<TaskItem> AssignedTasks { get; set; }
}
