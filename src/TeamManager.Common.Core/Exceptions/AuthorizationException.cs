using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Common.Core.Exceptions;

public class AuthorizationException : UnauthorizedException
{
    public AuthorizationException() : base("Unauthorized")
    {
    }
}