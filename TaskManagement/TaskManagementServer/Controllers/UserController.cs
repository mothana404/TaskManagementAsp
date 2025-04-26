using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementServer.Services.IService;
using TaskManagementServer.DTOs;
using TaskManagementServer.Models;
using TaskManagementServer.Extensions;

namespace TaskManagementServer.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("Register")]
    public async Task<ActionResult<string>> Register([FromBody] RegisterDto dto)
    {
        try
        {
            var token = await _userService.RegisterUser(dto);

            if (token == null)
                return BadRequest("User already exists or registration failed.");

            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _userService.LoginUser(dto);

            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("UserPagination")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<User>> AllUsers([FromQuery] PaginationDto pagination)
    {
        try
        {
            var users = await _userService.GetAllUsers(pagination);

            if (users.Items.Count == 0)
                return NotFound("No Users Data!");

            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetUserProfile(int id)
    {
        try
        {
            var userData = await _userService.GetUserById(id);

            if (userData == null)
                return NotFound("User not found.");

            return Ok(userData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UpdateUserDto>> UpdateUser([FromBody] UpdateUserDto dto)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return Unauthorized("Invalid user ID.");

            var updatedUser = await _userService.UpdateUserData(dto, userId.Value);

            if (updatedUser == null)
                return NotFound("User not found.");

            return Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var updatedUser = await _userService.DeleteUserById(id);

            if (updatedUser == false)
                return NotFound("User Account found.");

            return Ok("User deleted successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}