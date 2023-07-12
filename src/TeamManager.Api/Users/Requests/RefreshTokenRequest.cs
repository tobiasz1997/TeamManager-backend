using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Users.Requests;

public class RefreshTokenRequest
{
    [property: Required] 
    public string RefreshToken { get; init; }
};