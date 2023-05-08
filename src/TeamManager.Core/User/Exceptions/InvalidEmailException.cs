using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.Exceptions;

public sealed class InvalidEmailException : CustomException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email:'{email}' is invalid.")
    {
        Email = email;
    }
}