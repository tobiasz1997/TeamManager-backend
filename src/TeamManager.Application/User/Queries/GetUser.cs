using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Application.User.DTO;

namespace TeamManager.Application.User.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}