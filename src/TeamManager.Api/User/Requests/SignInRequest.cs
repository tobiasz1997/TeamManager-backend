using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.User.Requests;

public class SignInRequest
{

    [property: Required] 
    public string Email { get; set; }
    [property: Required] 
    public string Password { get; set; }
};