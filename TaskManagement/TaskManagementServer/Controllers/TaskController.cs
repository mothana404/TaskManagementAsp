using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementServer.DTOs;
using TaskManagementServer.Enums;
using TaskManagementServer.Extensions;
using TaskManagementServer.Models;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService;

    [HttpPost("CreateTask/{teamId}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskDto dto, int teamId)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var newTask = await _taskService.CreateNewTask(dto, teamId);

            if (newTask == null)
                return BadRequest("Task Already Exist!");

            return Ok(newTask);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateTask/{teamId}")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult<TaskItem>> UpdateTask(TaskDto dto, int teamId)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var UpdatedTask = await _taskService.UpdateTaskData(dto, teamId);

            if (UpdatedTask == null)
                return BadRequest("Task does not exist!");

            return Ok(UpdatedTask);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{teamId}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteTask(int teamId)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var UpdatedTask = await _taskService.DeleteTask(teamId);

            if (!UpdatedTask)
                return BadRequest("Task Is Not Exist!");

            return Ok("Deleted Successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AllTasks/{teamId}")]
    [Authorize(Roles = "Member, Manager")]
    public async Task<ActionResult<IEnumerable<AllTasksItemsDto>>> AllTasks(int teamId)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var allTasks = await _taskService.GetAllTasks(teamId);

            return Ok(allTasks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("MyTasks/{teamId}")]
    [Authorize(Roles = "Member, Manager, Admin")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> UserTasks(int teamId)
    {
        try
        {
            var userId = User.GetUserId();

            if (userId == null)
                return BadRequest("User ID claim not found");

            var userTasks = await _taskService.GetUserTasks(teamId, userId.Value);

            return Ok(userTasks);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("StartTask/{taskId}")]
    [Authorize(Roles = "Member, Manager")]
    public async Task<IActionResult> StartTask(int taskId)
    {
        try
        {
            var result = await _taskService.UpdateTaskStatus(taskId, TaskItem_Status.InProgress);

            if (!result)
                return NotFound("Task not found");

            return Ok("Task started and is now In Progress");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("CompleteTask/{taskId}")]
    [Authorize(Roles = "Member, Manager, Admin")]
    public async Task<IActionResult> CompleteTask(int taskId)
    {
        try
        {
            var result = await _taskService.UpdateTaskStatus(taskId, TaskItem_Status.Done);

            if (!result)
                return NotFound("Task not found");

            return Ok("Task completed and marked as Done");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("ReturnTask/{taskId}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> ReturnTask(int taskId)
    {
        try
        {
            var result = await _taskService.UpdateTaskStatus(taskId, TaskItem_Status.InProgress);

            if (!result)
                return NotFound("Task not found");

            return Ok("Task returned to In Progress");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
