using MediatR;
using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class UpdateAssignmentStatusHandler : ICommandHandler<UpdateAssignmentStatus>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public UpdateAssignmentStatusHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Unit> Handle(UpdateAssignmentStatus request, CancellationToken cancellationToken)
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