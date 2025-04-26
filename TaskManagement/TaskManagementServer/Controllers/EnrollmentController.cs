using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementServer.Extensions;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Controllers;

[Route("[controller]")]
[ApiController]
public class EnrollmentController(IEnrollmentService enrollmentService) : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService = enrollmentService;

    [HttpPost("Request/{id}")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> JoinTeam(int id)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var newTeam = await _enrollmentService.JoinRequest(userId.Value, id);

            if (!newTeam)
                return BadRequest("Team Not Exist Or You Already Joined!");

            return Ok("Request Have Been Send.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Leave/{id}")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> LeaveTeam(int id)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var result = await _enrollmentService.RemoveJoiningTeam(userId.Value, id);

            if (!result)
                return BadRequest("You are not joined");

            return Ok("You have successfully canceled the team or canceled request.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
