using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Timers.Repositories;

public interface IProjectRepositoryQueries
{
    Task<IEnumerable<Models.Project>> GetAllAsync(Id userId);
}