using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresTimerRepositoryQueries : ITimerRepositoryQueries
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresTimerRepositoryQueries(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Timer?> GetAsync(Id id) => _dbContext.Timers
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Timer>> GetAllAsync(Id userId, Guid? projectId, DateTime? startDate, DateTime? endDate)
    {
        var result = await _dbContext.Timers
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Where(x => projectId == null || x.ProjectId == projectId)
            .Where(x => startDate == null || x.Date >= startDate)
            .Where(x => endDate == null || x.Date <= endDate)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
        return result;
    }
}