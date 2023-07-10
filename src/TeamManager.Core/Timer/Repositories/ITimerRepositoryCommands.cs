using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Timer.Repositories;

public interface ITimerRepositoryCommands
{
    Task<Models.Timer?> GetAsync(Id id);
    Task CreateAsync(Models.Timer timer);
    Task UpdateAsync(Models.Timer timer);
    Task DeleteAsync(Models.Timer timer);
}