using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;

namespace TeamManager.Application.Shared.Services;

public interface IRefreshTokenService
{
    RefreshToken Create(Id userId);
    
    Task<RefreshToken> CreateOrRefresh(Id userId);
    
    RefreshToken Refresh(RefreshToken token);
}