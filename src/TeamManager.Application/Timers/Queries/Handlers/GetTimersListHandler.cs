using Mediator;
using TeamManager.Application.Timers.DTO;
using TeamManager.Application.Timers.Mappers;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Application.Timers.Queries.Handlers;

public class GetTimersListHandler : IRequestHandler<GetTimersList, PagedResult<TimerDto>>
{
    private readonly ITimerRepositoryQueries _timerRepository;
    private readonly IProjectRepositoryQueries _projectRepository;

    public GetTimersListHandler(ITimerRepositoryQueries timerRepository, IProjectRepositoryQueries projectRepository)
    {
        _timerRepository = timerRepository;
        _projectRepository = projectRepository;
    }

    public async ValueTask<PagedResult<TimerDto>> Handle(GetTimersList request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync(request.UserId);
        var timers = await _timerRepository
            .GetAllAsync(request.UserId, request.ProjectId, request.StartDate, request.EndDate);
        var mappedTimers = timers.Select(x => x.AsDto(projects)).ToList();
        var results = new PagedResult<TimerDto>(mappedTimers.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize), mappedTimers.Count());

        return results;
    }
}