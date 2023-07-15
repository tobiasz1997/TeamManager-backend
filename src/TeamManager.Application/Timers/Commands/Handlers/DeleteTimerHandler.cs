using MediatR;
using TeamManager.Application.Timers.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class DeleteTimerHandler : ICommandHandler<DeleteTimer>
{
    private readonly ITimerRepositoryCommands _projectRepository;

    public DeleteTimerHandler(ITimerRepositoryCommands projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(DeleteTimer request, CancellationToken cancellationToken)
    {
        var result = await _projectRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new TimerNotFoundException(request.Id);
        }
        
        await _projectRepository.DeleteAsync(result);
        return Unit.Value;
    }
}