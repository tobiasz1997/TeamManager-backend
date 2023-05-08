using Microsoft.AspNetCore.Http;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.User.DTO;

namespace TeamManager.Infrastructure.Shared.Auth;

internal sealed class AccessTokenStorage : IAccessTokenStorage
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string TokenKey = "jwt";

    public AccessTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(AuthResultDto jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

    public AuthResultDto Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as AuthResultDto;
        }

        return null;
    }
}