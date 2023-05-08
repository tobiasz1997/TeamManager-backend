using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.User.DTO;

public class UserDto
{
    [property: Required]
    public Guid Id { get;  set; }
    [property: Required]
    public string Email { get;  set; }
    [property: Required]
    public string FirstName { get;  set; }
    [property: Required]
    public string LastName { get;  set; }
}