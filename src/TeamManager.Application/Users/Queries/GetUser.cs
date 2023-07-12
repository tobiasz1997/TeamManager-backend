using TeamManager.Application.Users.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Users.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; init; }
}