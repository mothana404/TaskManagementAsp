using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Data;
using TaskManagementServer.DTOs;
using TaskManagementServer.Models;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Services;

public class TeamService(ApplicationDbContext context) : ITeamService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Team?> CreateTeam(CreateTeamDto dto, int userId)
    {
        bool teamExists = await _context.Teams.AnyAsync(t => t.Name == dto.Name);

        if (teamExists)
            return null;

        var newTeam = new Team
        {
            Name = dto.Name,
            Description = dto.Description,
            TeamManagerId = userId,
        };

        await _context.Teams.AddAsync(newTeam);
        await _context.SaveChangesAsync();

        return newTeam;
    }

    public Task<IEnumerable<Team>> GetAllTeams()
    {
        throw new NotImplementedException();
    }

    public async Task<Team?> UpdateTeamData(UpdateTeamDto dto, int teamId, int userId)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

        if (team == null || team.TeamManagerId != userId)
            return null;

        team.Description = dto.Description;
        team.Name = dto.Name;

        await _context.SaveChangesAsync();
        return team;
    }

    public async Task<bool> DeleteTeamData(int teamId, int userId)
    {
        var deletedTeam = await _context.Teams.FindAsync(teamId);

        if (deletedTeam == null)
            return false;

        _context.Teams.Remove(deletedTeam);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Team>?> GetManagerTeams(int userId)
    {
        return await _context.Teams.Where(t => t.TeamManagerId == userId)
            .Select(t => new Team
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
            }).ToListAsync();
    }

    public async Task<IEnumerable<Team>?> GetUserTeams(int userId)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == userId)
            .Select(e => new Team
            {
                Id = e.Team.Id,
                Name = e.Team.Name,
                Description = e.Team.Description
            })
            .ToListAsync();
    }
}
