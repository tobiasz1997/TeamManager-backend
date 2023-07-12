using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Models;
using TeamManager.Core.Users.ValueObjects;

namespace TeamManager.Core.Users.Repositories;

public interface IRefreshTokenRepositoryCommands
{
    Task<RefreshToken?> GetByToken(Token token);
    Task<RefreshToken?> GetByUserId(Id userId);
    Task Insert(RefreshToken refreshToken);
    Task<bool> IsInUse(Id id, Id userId, Token token);
    Task Update(RefreshToken token);
}