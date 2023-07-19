using Mediator;
using TeamManager.Application.Timers.DTO;

namespace TeamManager.Application.Timers.Queries;

public class GetProjectsList : IRequest<IEnumerable<ProjectDto>>
{
    public Guid UserId { get; init; }
}