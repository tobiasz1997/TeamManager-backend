using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Application.User.Exceptions;

public class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }
    
    public EmailAlreadyInUseException(string email) : base($"Email {email} is already in use.")
    {
        Email = email;
    }
}