using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.User.Requests;

public class RefreshTokenRequest
{
    [property: Required] 
    public string RefreshToken { get; init; }
};