using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Users.Requests;

public class SignInRequest
{

    [property: Required] 
    public string Email { get; init; }
    [property: Required] 
    public string Password { get; init; }
};