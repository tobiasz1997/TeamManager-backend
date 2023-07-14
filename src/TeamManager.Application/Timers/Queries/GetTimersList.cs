using TeamManager.Application.Timers.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Timers.Queries;

public class GetTimersList : IQuery<PagedResult<TimerDto>>
{
    public Guid UserId { get; init; }
    public Guid? ProjectId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}