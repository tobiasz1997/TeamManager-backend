using Mediator;
using TeamManager.Application.Timers.DTO;
using TeamManager.Common.Core.Browsing;

namespace TeamManager.Application.Timers.Queries;

public class GetTimersList : IRequest<PagedResult<TimerDto>>
{
    public Guid UserId { get; init; }
    public Guid? ProjectId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}