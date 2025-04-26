using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Data;
using TaskManagementServer.DTOs;
using TaskManagementServer.Enums;
using TaskManagementServer.Models;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Services;

public class TaskService(ApplicationDbContext context) : ITaskService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<TaskItem?> CreateNewTask(TaskDto dto, int teamId)
    {
        if (await _context.TaskItems.AnyAsync(t => t.Title == dto.Title && t.TeamId == teamId))
            return null;

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            AssignedUserId = dto.AssignedUserId,
            TeamId = teamId,
            DueDate = dto.DueDate
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem?> UpdateTaskData(TaskDto dto, int teamId)
    {
        var task = await _context.TaskItems
            .FirstOrDefaultAsync(t => t.Title == dto.Title && t.TeamId == teamId);

        if (task == null)
            return null;

        task.Description = dto.Description;
        task.Status = dto.Status;
        task.AssignedUserId = dto.AssignedUserId;
        task.DueDate = dto.DueDate;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int teamId)
    {
        var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.TeamId == teamId);

        if (task == null)
            return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AllTasksItemsDto>> GetAllTasks(int teamId)
    {
        var allTasks = await _context.TaskItems
            .Where(t => t.TeamId == teamId)
            .Include(t => t.AssignedUser)
            .Select(t => new AllTasksItemsDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                AssignedUserId = t.AssignedUserId,
                AssignedUsername = t.AssignedUser != null ? t.AssignedUser.UserName : null,
                DueDate = t.DueDate
            })
            .ToListAsync();

        return allTasks;
    }

    public async Task<IEnumerable<AllTasksItemsDto>> GetUserTasks(int teamId, int userId)
    {
        var userTasks = await _context.TaskItems
            .Where(t => t.TeamId == teamId && t.AssignedUserId == userId)
            .Include(t => t.AssignedUser)
            .Select(t => new AllTasksItemsDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                AssignedUserId = t.AssignedUserId,
                AssignedUsername = t.AssignedUser != null ? t.AssignedUser.UserName : null,
                DueDate = t.DueDate
            })
            .ToListAsync();

        return userTasks;
    }

    public async Task<bool> UpdateTaskStatus(int taskId, TaskItem_Status newStatus)
    {
        var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == taskId);

        if (task == null)
            return false;

        task.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }
}
