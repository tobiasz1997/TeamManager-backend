using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class DeleteAssignmentHandler : ICommandHandler<DeleteAssignment>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public DeleteAssignmentHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task HandleAsync(DeleteAssignment command)
    {
        var result = await _assignmentRepository.GetAsync(command.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(command.Id);
        }
        
        await _assignmentRepository.DeleteAsync(result);
    }
}