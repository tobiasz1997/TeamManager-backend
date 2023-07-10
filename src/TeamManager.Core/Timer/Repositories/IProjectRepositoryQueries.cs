using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Timer.Repositories;

public interface IProjectRepositoryQueries
{
    Task<IEnumerable<Models.Project>> GetAllAsync(Id userId);
}