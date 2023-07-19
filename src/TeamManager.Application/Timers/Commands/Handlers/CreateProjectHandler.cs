using Mediator;
using TeamManager.Application.Shared.Services;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class CreateProjectHandler : ICommandHandler<CreateProject>
{
    private readonly IClock _clock;
    private readonly IProjectRepositoryCommand _projectRepository;

    public CreateProjectHandler(IClock clock, IProjectRepositoryCommand projectRepository)
    {
        _clock = clock;
        _projectRepository = projectRepository;
    }

    public async ValueTask<Unit> Handle(CreateProject request, CancellationToken cancellationToken)
    {
        var newProject = new Project(request.Id, request.UserId, request.Label, request.Color, _clock.Current());
        await _projectRepository.CreateAsync(newProject);
        return Unit.Value;
    }
}