using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Application.Assignments.Exceptions;

public class AssignmentNotFoundException : NotFoundException
{
    public Guid Id { get;  }
    
    public AssignmentNotFoundException(Guid id) : base($"Assignment with id = {id} is not exist.")
    {
        Id = id;
    }
}