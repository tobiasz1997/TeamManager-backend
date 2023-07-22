using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;

namespace TeamManager.Core.Timers.Repositories;

public interface IProjectRepositoryCommand
{
    Task<Project?> GetAsync(Id id);
    Task CreateAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
}