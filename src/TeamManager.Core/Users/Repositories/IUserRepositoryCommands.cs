using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.ValueObjects;

namespace TeamManager.Core.Users.Repositories;

public interface IUserRepositoryCommands
{
    Task<Models.User?> GetByIdAsync(Id id);
    Task<Models.User?> GetByEmailAsync(Email id);
    Task AddAsync(Models.User user);
    Task UpdateAsync(Models.User user);
}