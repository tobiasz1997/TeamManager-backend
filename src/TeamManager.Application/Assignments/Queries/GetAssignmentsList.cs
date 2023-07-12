using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Common.MediatR.Queries;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignmentsList : IQuery<PagedResult<AssignmentDto>>
{
    public Guid UserId { get; init; }
    public AssignmentStatusType Type { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}