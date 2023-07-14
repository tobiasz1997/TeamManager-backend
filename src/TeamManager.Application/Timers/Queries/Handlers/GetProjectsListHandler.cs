using TeamManager.Application.Shared.Services;
using TeamManager.Application.Timers.DTO;
using TeamManager.Application.Timers.Mappers;
using TeamManager.Common.MediatR.Queries;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Queries.Handlers;

public class GetProjectsListHandler : IQueryHandler<GetProjectsList, IEnumerable<ProjectDto>>
{
    private readonly IProjectRepositoryQueries _projectRepository;

    public GetProjectsListHandler(IProjectRepositoryQueries projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsList request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(new Id(request.UserId));
        var result = projects.Select(x => x.AsDto());
        return result;
    }
}