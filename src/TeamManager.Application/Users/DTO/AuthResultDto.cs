using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.Users.DTO;

public class AuthResultDto
{
    [property: Required]
    public string AccessToken { get; init; } = string.Empty;

    [property: Required]
    public string RefreshToken { get; init; } = string.Empty;
}