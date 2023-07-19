using Mediator;
using TeamManager.Application.Timers.Exceptions;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class UpdateProjectHandler : ICommandHandler<UpdateProject>
{
    private readonly IProjectRepositoryCommand _projectRepository;

    public UpdateProjectHandler(IProjectRepositoryCommand projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<Unit> Handle(UpdateProject request, CancellationToken cancellationToken)
    {
        var result = await _projectRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new ProjectNotFoundException(request.Id);
        }
        
        result.Update(request.Id, request.Label, request.Color);
        await _projectRepository.UpdateAsync(result);
        return Unit.Value;
    }
}