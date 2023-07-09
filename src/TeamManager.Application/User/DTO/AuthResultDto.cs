using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.User.DTO;

public class AuthResultDto
{
    [property: Required]
    public string AccessToken { get; init; }

    [property: Required]
    public string RefreshToken { get; init; }
}