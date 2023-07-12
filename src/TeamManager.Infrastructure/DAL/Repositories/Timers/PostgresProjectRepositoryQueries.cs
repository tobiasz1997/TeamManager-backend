using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresProjectRepositoryQueries : IProjectRepositoryQueries
{
    public Task<IEnumerable<Project>> GetAllAsync(Id userId)
    {
        throw new NotImplementedException();
    }
}