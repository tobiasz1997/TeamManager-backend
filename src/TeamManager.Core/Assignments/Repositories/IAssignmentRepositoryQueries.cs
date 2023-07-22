using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Core.Assignments.Repositories;

public interface IAssignmentRepositoryQueries
{
    Task<Models.Assignment?> GetAsync(Id id);
    Task<IEnumerable<Models.Assignment>> GetAllAsync(Id userId, AssignmentStatusType? status = null);
}