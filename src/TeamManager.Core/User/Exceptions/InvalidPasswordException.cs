using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.Exceptions;

public class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base($"Password is invalid.")
    {

    }
}