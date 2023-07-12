using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresTimerRepositoryCommands : ITimerRepositoryCommands
{
    public Task<Timer?> GetAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Timer timer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Timer timer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Timer timer)
    {
        throw new NotImplementedException();
    }
}