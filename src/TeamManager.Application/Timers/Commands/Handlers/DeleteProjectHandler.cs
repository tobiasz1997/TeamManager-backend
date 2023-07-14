using MediatR;
using TeamManager.Application.Timers.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Commands.Handlers;

public class DeleteProjectHandler : ICommandHandler<DeleteProject>
{
    private readonly IProjectRepositoryCommand _projectRepository;

    public DeleteProjectHandler(IProjectRepositoryCommand projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Unit> Handle(DeleteProject request, CancellationToken cancellationToken)
    {
        var result = await _projectRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new ProjectNotFoundException(request.Id);
        }
        
        await _projectRepository.DeleteAsync(result);
        return Unit.Value;
    }
}