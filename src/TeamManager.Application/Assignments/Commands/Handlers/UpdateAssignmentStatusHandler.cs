using Mediator;
using TeamManager.Application.Assignments.Exceptions;
using TeamManager.Core.Assignments.Repositories;

namespace TeamManager.Application.Assignments.Commands.Handlers;

public sealed class UpdateAssignmentStatusHandler : ICommandHandler<UpdateAssignmentStatus>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public UpdateAssignmentStatusHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<Unit> Handle(UpdateAssignmentStatus request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(request.Id);
        }

        result.UpdateAssignmentStatus(request.Status);
        await _assignmentRepository.UpdateAsync(result);
        return Unit.Value;
    }
}