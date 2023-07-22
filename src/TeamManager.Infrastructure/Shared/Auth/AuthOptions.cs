namespace TeamManager.Infrastructure.Shared.Auth;

public class AuthOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SigningKey { get; set; } = string.Empty;
    public int AccessTokenExpiryInMinutes { get; set; }
    public int RefreshTokenExpiryInDays { get; set; }
}