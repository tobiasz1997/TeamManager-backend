using TeamManager.Application.User.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.User.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; init; }
}