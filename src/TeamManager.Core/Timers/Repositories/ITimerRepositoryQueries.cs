using TeamManager.Core.Shared.ValueObjects;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Core.Timers.Repositories;

public interface ITimerRepositoryQueries
{
    Task<Timer?> GetAsync(Id id);
    Task<IEnumerable<Timer>> GetAllAsync(Id userId, Guid? projectId, DateTime? startDate, DateTime? endDate);
}