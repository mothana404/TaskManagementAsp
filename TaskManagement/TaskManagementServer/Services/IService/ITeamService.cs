using TaskManagementServer.DTOs;
using TaskManagementServer.Models;

namespace TaskManagementServer.Services.IService;

public interface ITeamService
{
    Task<Team?> CreateTeam(CreateTeamDto dto, int userId);
    Task<IEnumerable<Team>> GetAllTeams();
    Task<bool> DeleteTeamData(int teamId, int userId);
    Task<Team?> UpdateTeamData(UpdateTeamDto dto, int teamId, int userId);
    Task<IEnumerable<Team>?> GetManagerTeams(int userId);
    Task<IEnumerable<Team>?> GetUserTeams(int userId);
}
