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

    public async Task<IEnumerable<Timer>> GetAllAsync(Id userId)
    {
        var result = await _dbContext.Timers
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        return result;
    }
}