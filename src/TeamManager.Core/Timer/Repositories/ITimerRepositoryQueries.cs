using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Timer.Repositories;

public interface ITimerRepositoryQueries
{
    Task<Models.Timer?> GetAsync(Id id);
    Task<IEnumerable<Models.Timer>> GetAllAsync(Id userId);
}