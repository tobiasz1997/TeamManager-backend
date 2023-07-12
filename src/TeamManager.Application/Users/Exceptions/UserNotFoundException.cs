using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Users.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public Guid Id { get;  }
    
    public UserNotFoundException(Guid id) : base($"User with id = {id} is not exist.")
    {
        Id = id;
    }
}