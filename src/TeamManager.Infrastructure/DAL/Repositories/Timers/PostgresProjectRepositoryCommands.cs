using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

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