using TeamManager.Application.Shared.Abstractions.Exceptions;

namespace TeamManager.Api.Shared;

public class AuthorizationException : UnauthorizedException
{
    public AuthorizationException() : base("Unauthorized")
    {
    }
}