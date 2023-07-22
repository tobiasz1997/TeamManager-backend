using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Assignments.Models;
using TeamManager.Core.Assignments.Repositories;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Infrastructure.DAL.Repositories.Assignments;

internal sealed class PostgresAssignmentRepositoryQueries : IAssignmentRepositoryQueries
{
    private readonly TeamManagerDbContext _dbContext;
    
    public PostgresAssignmentRepositoryQueries(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Assignment?> GetAsync(Id id) => _dbContext.Assignments
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Assignment>> GetAllAsync(Id userId, AssignmentStatusType? status = null)
    {
        var results = await _dbContext.Assignments
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Where(x => status == null || x.Status == status)
            .OrderBy(x => x.StartDate)
            .ToListAsync();
        return results;
    }
}