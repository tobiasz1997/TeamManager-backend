using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.ValueObjects;

namespace TeamManager.Core.Users.Models;

public class User
{
    public Id Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public User(Id id, Email email, Password password, FirstName firstName, LastName lastName, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
    }
}