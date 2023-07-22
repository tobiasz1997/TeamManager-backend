using Mediator;
using TeamManager.Application.Users.DTO;

namespace TeamManager.Application.Users.Queries;

public class GetUser : IRequest<UserDto>
{
    public Guid UserId { get; init; }
}