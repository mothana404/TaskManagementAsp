using System.ComponentModel.DataAnnotations;
using TaskManagementServer.Enums;

namespace TaskManagementServer.DTOs;

public class RegisterDto
{
    [MinLength(6, ErrorMessage = "UserName must be at least 6 characters")]
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
    public string Password { get; set; }

    public List<UserRoles> Roles { get; set; } = new List<UserRoles>();
}