using TaskManagementServer.Enums;

namespace TaskManagementServer.Models;

public class Role
{
    public int Id { get; set; }

    public UserRoles RoleName { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
