using Microsoft.EntityFrameworkCore;
using TaskManagementServer.Data;
using TaskManagementServer.Models;
using TaskManagementServer.Services.IService;

namespace TaskManagementServer.Services;

public class EnrollmentService(ApplicationDbContext context) : IEnrollmentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> JoinRequest(int userId, int teamId)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

        if (team == null)
            return false;

        bool isEnrolled = await _context.Enrollments
            .AnyAsync(e => e.UserId == userId && e.TeamId == teamId);

        if (isEnrolled)
            return false;

        bool requestExists = await _context.TeamRequests
            .AnyAsync(r => r.UserId == userId && r.TeamId == teamId);

        if (requestExists)
            return false; 

        var teamRequest = new TeamRequest
        {
            UserId = userId,
            TeamId = teamId,
        };

        _context.TeamRequests.Add(teamRequest);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveJoiningTeam(int userId, int teamId)
    {
        var existingRequest = await _context.TeamRequests
            .FirstOrDefaultAsync(r => r.UserId == userId && r.TeamId == teamId);

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.TeamId == teamId && e.UserId == userId);

        if (existingRequest == null && enrollment == null)
            return false; 

        if (existingRequest != null)
            _context.TeamRequests.Remove(existingRequest);

        if (enrollment != null)
            _context.Enrollments.Remove(enrollment);

        await _context.SaveChangesAsync();
        return true; 
    }
}
