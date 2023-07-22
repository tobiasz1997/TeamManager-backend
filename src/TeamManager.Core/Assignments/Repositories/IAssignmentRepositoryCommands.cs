using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Assignments.Repositories;

public interface IAssignmentRepositoryCommands
{
    Task<Models.Assignment?> GetAsync(Id id);
    Task CreateAsync(Models.Assignment assignment);
    Task UpdateAsync(Models.Assignment assignment);
    Task DeleteAsync(Models.Assignment assignment);
}