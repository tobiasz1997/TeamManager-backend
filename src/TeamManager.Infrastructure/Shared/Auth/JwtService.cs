using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TeamManager.Application.Shared.Services;

namespace TeamManager.Infrastructure.Shared.Auth;

internal sealed class JwtService : IJwtService
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiry;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    public JwtService(IOptions<AuthOptions> options, IClock clock)
    {
        _clock = clock;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.AccessTokenExpiryInMinutes;
        _signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
                SecurityAlgorithms.HmacSha256);
    }

    public string CreateToken(Guid userId, string email)
    {
        var now = _clock.Current();
        var expires = now.AddMinutes(_expiry);
        
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Email, email),
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

        return accessToken;
    }
}