using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresProjectRepositoryQueries : IProjectRepositoryQueries
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresProjectRepositoryQueries(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllAsync(Id userId)
    {
        var result = await _dbContext.Projects
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        return result;
    }
}