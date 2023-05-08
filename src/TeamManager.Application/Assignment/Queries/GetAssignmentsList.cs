using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Browsing;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Core.User.Enums;

namespace TeamManager.Application.Assignment.Queries;

public class GetAssignmentsList : IQuery<PagedResult<AssignmentDto>>
{
    public Guid UserId { get; set; }
    public AssignmentStatusType Type { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}