using MediatR;
using TeamManager.Application.Shared.Services;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Commands.Handlers;

public sealed class CreateAssignmentHandler : ICommandHandler<CreateAssignment>
{
    private readonly IClock _clock;
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public CreateAssignmentHandler(IClock clock, IAssignmentRepositoryCommands assignmentRepository)
    {
        _clock = clock;
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Unit> Handle(CreateAssignment request, CancellationToken cancellationToken)
    {
        var newAssignment = new Core.Assignment.Models.Assignment(request.Id, request.UserId, request.Name, request.Description, request.Priority, request.Status, _clock.Current());
        await _assignmentRepository.CreateAsync(newAssignment);
        return Unit.Value;
    }
}