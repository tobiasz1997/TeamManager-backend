using Mediator;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignmentsList : IRequest<PagedResult<AssignmentDto>>
{
    public Guid UserId { get; init; }
    public AssignmentStatusType Type { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}