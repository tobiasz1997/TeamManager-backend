using MediatR;
using TeamManager.Application.Shared.Services;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Timers.Repositories;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class CreateTimerHandler : ICommandHandler<CreateTimer>
{
    private readonly IClock _clock;
    private readonly ITimerRepositoryCommands _projectRepository;

    public CreateTimerHandler(ITimerRepositoryCommands projectRepository, IClock clock)
    {
        _projectRepository = projectRepository;
        _clock = clock;
    }

    public async Task<Unit> Handle(CreateTimer request, CancellationToken cancellationToken)
    {
        var newProject = new Timer(request.Id, request.UserId,request.ProjectId, request.Description, request.Date, _clock.Current());
        await _projectRepository.CreateAsync(newProject);
        return Unit.Value;
    }
}