using Mediator;
using TeamManager.Application.Users.DTO;
using TeamManager.Application.Users.Exceptions;
using TeamManager.Core.Users.Repositories;

namespace TeamManager.Application.Users.Queries.Handlers;

public class GetUserHandler : IRequestHandler<GetUser, UserDto>
{
    private readonly IUserRepositoryQueries _userRepositoryQueries;

    public GetUserHandler(IUserRepositoryQueries userRepositoryQueries)
    {
        _userRepositoryQueries = userRepositoryQueries;
    }

    public async ValueTask<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryQueries.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}