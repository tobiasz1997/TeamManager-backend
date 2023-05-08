using TeamManager.Application.Shared.Abstractions.Exceptions;

namespace TeamManager.Application.Assignment.Exceptions;

public class AssignmentNotFoundException : NotFoundException
{
    public Guid Id { get;  }
    
    public AssignmentNotFoundException(Guid id) : base($"Assignment with id = {id} is not exist.")
    {
        Id = id;
    }
}