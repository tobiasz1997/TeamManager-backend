using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Core.Users.Exceptions;

public sealed class InvalidEmailException : MethodNotAllowedException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email:'{email}' is invalid.")
    {
        Email = email;
    }
}