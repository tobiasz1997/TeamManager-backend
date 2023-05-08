using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Core.User.Repositories;

public interface IRefreshTokenRepositoryCommands
{
    Task<RefreshToken?> GetByToken(Token token);
    Task<RefreshToken?> GetByUserId(Id userId);
    Task Insert(RefreshToken refreshToken);
    Task<bool> IsInUse(Id id, Id userId, Token token);
    Task Update(RefreshToken token);
}