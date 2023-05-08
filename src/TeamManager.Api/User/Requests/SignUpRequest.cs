using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.User.Requests;

public class SignUpRequest
{
    [property: Required]
    public string Email { get; set; }
    [property: Required]
    public string Password { get; set; } 
    [property: Required]
    public string FirstName { get; set; }
    [property: Required]
    public string LastName { get; set; }
}