namespace TeamManager.Application.Assignment.Commands.Handlers;

using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Assignment.Repositories;

public sealed class CreateAssignmentHandler : ICommandHandler<CreateAssignment>
{
    private readonly IClock _clock;
    private readonly IAssignmentRepositoryCommands _assignmentRepository;
    
    public CreateAssignmentHandler(IClock clock, IAssignmentRepositoryCommands assignmentRepository)
    {
        _clock = clock;
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task HandleAsync(CreateAssignment command)
    {
        var newAssignment = new Core.Assignment.Models.Assignment(command.Id, command.UserId, command.Name, command.Description, command.Priority, command.Status, _clock.Current());
        await _assignmentRepository.CreateAsync(newAssignment);
    }
}