#nullable enable
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Assignment.Repositories;

public interface IAssignmentRepositoryQueries
{
    Task<Models.Assignment?> GetAsync(Id id);
    Task<IEnumerable<Models.Assignment>> GetAllAsync();
}