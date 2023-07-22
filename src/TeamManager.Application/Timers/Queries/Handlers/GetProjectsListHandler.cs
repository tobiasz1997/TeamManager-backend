using Mediator;
using TeamManager.Application.Timers.DTO;
using TeamManager.Application.Timers.Mappers;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Queries.Handlers;

public class GetProjectsListHandler : IRequestHandler<GetProjectsList, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepositoryQueries _projectRepository;

    public GetProjectsListHandler(IProjectRepositoryQueries projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async ValueTask<IEnumerable<ProjectDto>> Handle(GetProjectsList request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(new Id(request.UserId));
        var result = projects.Select(x => x.AsDto());
        return result;
    }
}