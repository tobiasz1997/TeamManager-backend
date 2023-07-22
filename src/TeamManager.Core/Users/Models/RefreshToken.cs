using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.ValueObjects;

namespace TeamManager.Core.Users.Models;

public class RefreshToken
{

    public Id Id { get; private set; }
    public Id UserId { get; private set; }
    
    public Token Token { get; private set; }
    
    public DateTime ExpiresAt { get; private set; }
    
    public RefreshToken(Id id, Id userId, Token token, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void UpdateExpiryDate(DateTime dateTime)
    {
        ExpiresAt = dateTime;
    }
}