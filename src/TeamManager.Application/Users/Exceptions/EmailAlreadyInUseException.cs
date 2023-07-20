using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Users.Exceptions;

public class EmailAlreadyInUseException : MethodNotAllowedException
{
    public EmailAlreadyInUseException(string email) : base($"Email {email} is already in use.")
    {
    }
}