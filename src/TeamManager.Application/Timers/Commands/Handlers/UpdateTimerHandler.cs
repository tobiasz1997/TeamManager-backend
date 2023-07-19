using Mediator;
using TeamManager.Application.Timers.Exceptions;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class UpdateTimerHandler : ICommandHandler<UpdateTimer>
{
    private readonly ITimerRepositoryCommands _projectRepository;

    public UpdateTimerHandler(ITimerRepositoryCommands projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<Unit> Handle(UpdateTimer request, CancellationToken cancellationToken)
    {
        var result = await _projectRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new TimerNotFoundException(request.Id);
        }
        
        result.Update(request.ProjectId, request.Description, request.Date);
        await _projectRepository.UpdateAsync(result);
        return Unit.Value;
    }
}