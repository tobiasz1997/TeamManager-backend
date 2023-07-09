using MediatR;
using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class DeleteAssignmentHandler : ICommandHandler<DeleteAssignment>
{
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public DeleteAssignmentHandler(IAssignmentRepositoryCommands assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Unit> Handle(DeleteAssignment request, CancellationToken cancellationToken)
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