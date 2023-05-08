using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Core.User.Repositories;

public interface IUserRepositoryCommands
{
    Task<Models.User?> GetByIdAsync(Id id);
    Task<Models.User?> GetByEmailAsync(Email id);
    Task AddAsync(Models.User user);
    Task UpdateAsync(Models.User user);
}