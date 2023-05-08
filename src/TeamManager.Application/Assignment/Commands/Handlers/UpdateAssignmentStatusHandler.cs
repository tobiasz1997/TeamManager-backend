using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class UpdateAssignmentStatusHandler : ICommandHandler<UpdateAssignmentStatus>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public UpdateAssignmentStatusHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task HandleAsync(UpdateAssignmentStatus command)
    {
        var result = await _assignmentRepository.GetAsync(command.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(command.Id);
        }
        
        
        result.UpdateAssignmentStatus(command.Status);
        await _assignmentRepository.UpdateAsync(result);
    }
}