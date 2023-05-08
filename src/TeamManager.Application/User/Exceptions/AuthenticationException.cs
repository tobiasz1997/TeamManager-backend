using TeamManager.Application.Shared.Abstractions.Exceptions;
using TeamManager.Core.Shared.Exceptions;

namespace TeamManager.Application.User.Exceptions;

public class AuthenticationException : UnauthorizedException
{
    public AuthenticationException() : base("Unauthenticated")
    {
    }
}