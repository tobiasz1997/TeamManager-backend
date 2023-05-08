namespace TeamManager.Infrastructure.Shared.Auth;

public class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public int AccessTokenExpiryInMinutes { get; set; }
    public int RefreshTokenExpiryInDays { get; set; }
}