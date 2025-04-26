using TaskManagementServer.DTOs;
using TaskManagementServer.Enums;
using TaskManagementServer.Models;

namespace TaskManagementServer.Services.IService;

public interface ITaskService
{
    Task<TaskItem?> CreateNewTask(TaskDto dto, int teamId);
    Task<TaskItem?> UpdateTaskData(TaskDto dto, int teamId);
    Task<bool> DeleteTask(int teamId);
    Task<IEnumerable<AllTasksItemsDto>> GetAllTasks(int teamId);
    Task<IEnumerable<AllTasksItemsDto>> GetUserTasks(int teamId, int userId);
    Task<bool> UpdateTaskStatus(int taskId, TaskItem_Status newStatus);
}
