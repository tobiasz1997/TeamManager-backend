using Mediator;
using TeamManager.Application.Assignments.Exceptions;
using TeamManager.Core.Assignments.Repositories;

namespace TeamManager.Application.Assignments.Commands.Handlers;

public sealed class DeleteAssignmentHandler : ICommandHandler<DeleteAssignment>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public DeleteAssignmentHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<Unit> Handle(DeleteAssignment request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(request.Id);
        }
        
        await _assignmentRepository.DeleteAsync(result);
        return Unit.Value;
    }
}