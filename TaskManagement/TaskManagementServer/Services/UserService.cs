using TaskManagementServer.Data;
using TaskManagementServer.Models;
using TaskManagementServer.Services.Auth;
using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Enums;
using TaskManagementServer.DTOs;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Services;

public class UserService(ApplicationDbContext dbContext, AuthService authService) : IUserService
{
    private readonly ApplicationDbContext _context = dbContext;
    private readonly AuthService _authService = authService;

    public async Task<string?> LoginUser(LoginDto dto)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        return _authService.GenerateAccessToken(user);
    }

    public async Task<string?> RegisterUser(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return null;

        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            UserRoles = new List<UserRole>()
        };

        if (!dto.Roles.Any())
        {
            var memberRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == UserRoles.Member);

            if (memberRole != null)
                user.UserRoles.Add(new UserRole { Role = memberRole });
        }
        else
        {
            foreach (var roleEnum in dto.Roles)
            {
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.RoleName == roleEnum);

                if (role != null)
                    user.UserRoles.Add(new UserRole { Role = role });
            }
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _authService.GenerateAccessToken(user);
    }

    public async Task<PagedResult<User>> GetAllUsers(PaginationDto paginationParams)
    {
        var count = await _context.Users.CountAsync();
        var items = await _context.Users
            .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PagedResult<User>(items, count, paginationParams.PageNumber, paginationParams.PageSize);
    }

    public async Task<UserDto?> GetUserById(int userId)
    {
        var userProfile = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Teams = u.Enrollments.Select(e => new TeamSummaryDto
                {
                    Id = e.Team.Id,
                    Name = e.Team.Name,
                    Description = e.Team.Description,
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return userProfile;
    }

    public async Task<bool> DeleteUserById(int id)
    {
        var user = await _context.Users
            .Include(u => u.Enrollments)
            .Include(u => u.TeamRequests)
            .Include(u => u.AssignedTasks)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return false;

        _context.Enrollments.RemoveRange(user.Enrollments);
        _context.TaskItems.RemoveRange(user.AssignedTasks);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> UpdateUserData(UpdateUserDto dto, int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        if (user.UserName != dto.UserName)
            user.UserName = dto.UserName;

        if (user.Email != dto.Email)
            user.Email = dto.Email;

        await _context.SaveChangesAsync();

        return user;
    }
}