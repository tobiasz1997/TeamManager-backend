using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.User.Exceptions;

public class EmailAlreadyInUseException : MethodNotAllowedException
{
    public string Email { get; }
    
    public EmailAlreadyInUseException(string email) : base($"Email {email} is already in use.")
    {
        Email = email;
    }
}