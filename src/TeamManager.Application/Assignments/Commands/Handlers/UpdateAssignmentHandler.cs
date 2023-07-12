using MediatR;
using TeamManager.Application.Assignments.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Assignments.Repositories;

namespace TeamManager.Application.Assignments.Commands.Handlers;

public sealed class UpdateAssignmentHandler : ICommandHandler<UpdateAssignment>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public UpdateAssignmentHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Unit> Handle(UpdateAssignment request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(request.Id);
        }
        
        result.UpdateAssignment(request.Name, request.Description, request.Priority);
        await _assignmentRepository.UpdateAsync(result);
        return Unit.Value;
    }
}