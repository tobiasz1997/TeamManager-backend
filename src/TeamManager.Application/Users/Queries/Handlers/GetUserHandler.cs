using TeamManager.Application.Users.DTO;
using TeamManager.Application.Users.Exceptions;
using TeamManager.Common.MediatR.Queries;
using TeamManager.Core.Users.Repositories;

namespace TeamManager.Application.Users.Queries.Handlers;

public class GetUserHandler :IQueryHandler<GetUser, UserDto>
{
    private readonly IUserRepositoryQueries _userRepositoryQueries;

    public GetUserHandler(IUserRepositoryQueries userRepositoryQueries)
    {
        _userRepositoryQueries = userRepositoryQueries;
    }

    public async Task<UserDto> Handle(GetUser request, CancellationToken cancellationToken)
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