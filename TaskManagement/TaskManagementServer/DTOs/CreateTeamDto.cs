using System.ComponentModel.DataAnnotations;

namespace TaskManagementServer.DTOs;

public class CreateTeamDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(250, MinimumLength = 3)]
    public string Description { get; set; }
}
