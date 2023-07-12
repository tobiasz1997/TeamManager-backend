using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Users.Exceptions;

public class InvalidPasswordException : MethodNotAllowedException
{
    public InvalidPasswordException() : base($"Password is invalid.")
    {

    }
}