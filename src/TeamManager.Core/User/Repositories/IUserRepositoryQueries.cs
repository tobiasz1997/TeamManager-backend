using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.User.Repositories;

public interface IUserRepositoryQueries
{
    Task<Models.User?> GetByIdAsync(Id id);
}