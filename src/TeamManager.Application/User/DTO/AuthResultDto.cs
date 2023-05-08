using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.User.DTO;

public class AuthResultDto
{
    [property: Required]
    public string AccessToken { get; set; }
    [property: Required]
    public string RefreshToken { get; set; }
}