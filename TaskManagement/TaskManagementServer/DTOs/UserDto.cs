namespace TaskManagementServer.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public List<TeamSummaryDto> Teams { get; set; } = new List<TeamSummaryDto>();
}

public class TeamSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

