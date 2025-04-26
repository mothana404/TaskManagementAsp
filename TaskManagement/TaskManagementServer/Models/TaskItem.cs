using TaskManagementServer.Enums;

namespace TaskManagementServer.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskItem_Status Status { get; set; }

    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }

    public DateTime DueDate { get; set; }
}
