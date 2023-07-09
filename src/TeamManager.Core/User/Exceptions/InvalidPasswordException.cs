using TeamManager.Common.Core.Exceptions.Abstractions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.Exceptions;

public class InvalidPasswordException : MethodNotAllowedException
{
    public InvalidPasswordException() : base($"Password is invalid.")
    {

    }
}