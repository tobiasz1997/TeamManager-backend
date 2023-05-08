using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class UpdateAssignmentHandler : ICommandHandler<UpdateAssignment>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public UpdateAssignmentHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task HandleAsync(UpdateAssignment command)
    {
        var result = await _assignmentRepository.GetAsync(command.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(command.Id);
        }
        
        result.UpdateAssignment(command.Name, command.Description, command.Priority);
        await _assignmentRepository.UpdateAsync(result);
    }
}