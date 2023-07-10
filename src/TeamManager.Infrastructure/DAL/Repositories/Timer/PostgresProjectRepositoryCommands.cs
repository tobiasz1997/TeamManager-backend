using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timer.Models;
using TeamManager.Core.Timer.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timer;

internal sealed class PostgresProjectRepositoryCommands : IProjectRepositoryCommand
{
    public Task<Project?> GetAsync(Id id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Project project)
    {
        throw new NotImplementedException();
    }
}