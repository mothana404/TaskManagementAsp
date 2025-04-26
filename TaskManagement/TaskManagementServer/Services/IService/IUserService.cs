using TaskManagementServer.DTOs;
using TaskManagementServer.Models;

namespace TaskManagementServer.Services.IService;

public interface IUserService
{
    Task<string?> RegisterUser(RegisterDto dto);
    Task<string?> LoginUser(LoginDto dto);
    Task<bool> DeleteUserById(int id);
    Task<UserDto?> GetUserById(int userId);
    Task<User?> UpdateUserData(UpdateUserDto dto, int userId);
    Task<PagedResult<User>> GetAllUsers(PaginationDto paginationDto);
}
