using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Users.Requests;

public class SignUpRequest
{
    [property: Required]
    public string Email { get; init; }
    [property: Required]
    public string Password { get; init; } 
    [property: Required]
    public string FirstName { get; init; }
    [property: Required]
    public string LastName { get; init; }
}