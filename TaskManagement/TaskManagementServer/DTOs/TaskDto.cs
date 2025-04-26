using TaskManagementServer.Enums;

namespace TaskManagementServer.DTOs;

public class TaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskItem_Status Status { get; set; }
    public int? AssignedUserId { get; set; }
    public DateTime DueDate { get; set; }
}
