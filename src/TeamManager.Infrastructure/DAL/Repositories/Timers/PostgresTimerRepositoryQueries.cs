using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

public class PostgresTimerRepositoryQueries : ITimerRepositoryQueries
{
    public Task<Timer?> GetAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Timer>> GetAllAsync(Id userId)
    {
        throw new NotImplementedException();
    }
}