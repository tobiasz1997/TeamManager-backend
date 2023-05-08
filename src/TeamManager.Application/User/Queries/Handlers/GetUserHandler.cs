using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Application.User.DTO;
using TeamManager.Application.User.Exceptions;
using TeamManager.Core.User.Repositories;

namespace TeamManager.Application.User.Queries.Handlers;

public class GetUserHandler :IQueryHandler<GetUser, UserDto>
{
    private readonly IUserRepositoryQueries _userRepositoryQueries;

    public GetUserHandler(IUserRepositoryQueries userRepositoryQueries)
    {
        _userRepositoryQueries = userRepositoryQueries;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _userRepositoryQueries.GetByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(query.UserId);
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