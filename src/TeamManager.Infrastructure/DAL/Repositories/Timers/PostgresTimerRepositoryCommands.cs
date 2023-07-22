using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresTimerRepositoryCommands : ITimerRepositoryCommands
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresTimerRepositoryCommands(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Timer?> GetAsync(Id id)
        => _dbContext.Timers.SingleOrDefaultAsync(x => x.Id == id);

    public Task CreateAsync(Timer timer)
    {
        _dbContext.Timers.AddAsync(timer);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Timer timer)
    {
        _dbContext.Timers.Update(timer);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Timer timer)
    {
        _dbContext.Timers.Remove(timer);
        return Task.CompletedTask;
    }
}