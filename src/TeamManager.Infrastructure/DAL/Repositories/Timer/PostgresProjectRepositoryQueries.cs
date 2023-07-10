using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timer.Models;
using TeamManager.Core.Timer.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timer;

internal sealed class PostgresProjectRepositoryQueries : IProjectRepositoryQueries
{
    public Task<IEnumerable<Project>> GetAllAsync(Id userId)
    {
        throw new NotImplementedException();
    }
}