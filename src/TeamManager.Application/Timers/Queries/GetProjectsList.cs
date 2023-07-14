using TeamManager.Application.Timers.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Timers.Queries;

public class GetProjectsList : IQuery<IEnumerable<ProjectDto>>
{
    public Guid UserId { get; init; }
}