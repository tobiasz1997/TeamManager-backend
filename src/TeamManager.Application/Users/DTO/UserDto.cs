using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.Users.DTO;

public class UserDto
{
    [property: Required]
    public Guid Id { get;  init; }
    [property: Required]
    public string Email { get;  init; } = string.Empty;

    [property: Required]
    public string FirstName { get;  init; } = string.Empty;

    [property: Required]
    public string LastName { get;  init; } = string.Empty;
}