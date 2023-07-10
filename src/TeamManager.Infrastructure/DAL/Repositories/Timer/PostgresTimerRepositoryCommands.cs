using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timer.Repositories;
using TeamManager.Core.Timer.Models;

namespace TeamManager.Infrastructure.DAL.Repositories.Timer;

internal sealed class PostgresTimerRepositoryCommands : ITimerRepositoryCommands
{
    public Task<Core.Timer.Models.Timer?> GetAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Core.Timer.Models.Timer timer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Core.Timer.Models.Timer timer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Core.Timer.Models.Timer timer)
    {
        throw new NotImplementedException();
    }
}