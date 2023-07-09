using TeamManager.Common.Core.Exceptions.Abstractions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Core.User.Exceptions;

public sealed class InvalidEmailException : MethodNotAllowedException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email:'{email}' is invalid.")
    {
        Email = email;
    }
}