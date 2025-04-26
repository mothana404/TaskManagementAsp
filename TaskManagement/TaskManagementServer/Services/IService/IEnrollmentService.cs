namespace TaskManagementServer.Services.IService;

public interface IEnrollmentService
{
    Task<bool> JoinRequest(int userId, int teamId);
    Task<bool> RemoveJoiningTeam(int userId, int id);
}
