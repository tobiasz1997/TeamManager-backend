using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timer.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timer;

public class PostgresTimerRepositoryQueries : ITimerRepositoryQueries
{
    public Task<Core.Timer.Models.Timer?> GetAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Core.Timer.Models.Timer>> GetAllAsync(Id userId)
    {
        throw new NotImplementedException();
    }
}