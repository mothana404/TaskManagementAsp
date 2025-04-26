using TaskManagementServer.Enums;

namespace TaskManagementServer.DTOs;

public class AllTasksItemsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? AssignedUserId { get; set; }
    public string? AssignedUsername { get; set; }
    public DateTime DueDate { get; set; }
}
