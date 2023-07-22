using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Models;

namespace TeamManager.Infrastructure.Shared.Auth;

internal sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IClock _clock;
    private readonly int _refreshTokenExpiry;

    public RefreshTokenService(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _refreshTokenExpiry = options.Value.RefreshTokenExpiryInDays;
    }

    public RefreshToken Create(Id userId)
    {
        var newToken = new RefreshToken(Guid.NewGuid(), userId,
            Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            _clock.Current().AddDays(_refreshTokenExpiry));
        
        return newToken;
    }

    public RefreshToken Refresh(RefreshToken token)
    {
        var newDate = _clock.Current().AddDays(_refreshTokenExpiry);
        token.UpdateExpiryDate(newDate);

        return token;
    }
}