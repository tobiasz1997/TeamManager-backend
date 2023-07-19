using Mediator;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Assignments.Models;
using TeamManager.Core.Assignments.Repositories;

namespace TeamManager.Application.Assignments.Commands.Handlers;

public sealed class CreateAssignmentHandler : ICommandHandler<CreateAssignment>
{
    private readonly IClock _clock;
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public CreateAssignmentHandler(IClock clock, IAssignmentRepositoryCommands assignmentRepository)
    {
        _clock = clock;
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<Unit> Handle(CreateAssignment request, CancellationToken cancellationToken)
    {
        var newAssignment = new Assignment(request.Id, request.UserId, request.Name, request.Description, request.Priority, request.Status, _clock.Current());
        await _assignmentRepository.CreateAsync(newAssignment);
        return Unit.Value;
    }
}