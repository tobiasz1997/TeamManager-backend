using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Core.Users.Repositories;

public interface IUserRepositoryQueries
{
    Task<Models.User?> GetByIdAsync(Id id);
}