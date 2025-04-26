using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementServer.DTOs;
using TaskManagementServer.Extensions;
using TaskManagementServer.Models;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Controllers;

[Route("[controller]")]
[ApiController]
public class TeamController(ITeamService teamService) : ControllerBase
{
    private readonly ITeamService _teamService = teamService;

    [HttpPost("createTeam")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<ActionResult<Team>> CreateTeam(CreateTeamDto dto)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var newTeam = await _teamService.CreateTeam(dto, userId.Value);

            if (newTeam == null) 
                return BadRequest("Team Name Already Exist!");

            return Ok(newTeam);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<ActionResult<Team>> UpdateTeam(int id, [FromBody] UpdateTeamDto dto)
    {
        try
        {
            var userId = User.GetUserId(); 
            if (userId == null)
                return BadRequest("User ID not found");

            var updatedTeam = await _teamService.UpdateTeamData(dto, id, userId.Value);

            if (updatedTeam == null)
                return NotFound("Team Not Found!");

            return Ok(updatedTeam);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }


    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        try
        {
            var userId = User.GetUserId();
            if (userId == null)
                return BadRequest("User ID not found");

            var deletedTeam = await _teamService.DeleteTeamData(id, userId.Value);

            if (!deletedTeam)
                return NotFound($"No team found with ID {id}");

            return Ok("Team Deleted Successfuly!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ManagerTeams")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> ManagerTeams()
    {
        try
        {
            var userId = User.GetUserId();
            if (userId == null)
                return BadRequest("User ID not found");

            var ManagerTeams = await _teamService.GetManagerTeams(userId.Value);

            if (ManagerTeams == null)
                return NotFound($"No Teams found.");

            return Ok(ManagerTeams);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("UserTeams")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> UserTeams()
    {
        try
        {
            var userId = User.GetUserId();
            if (userId == null)
                return BadRequest("User ID not found");

            var UserTeams = await _teamService.GetUserTeams(userId.Value);

            if (UserTeams == null)
                return NotFound($"No teams found.");

            return Ok(UserTeams);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
