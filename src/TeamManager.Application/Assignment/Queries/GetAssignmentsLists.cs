using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Queries;

namespace TeamManager.Application.Assignment.Queries;

public class GetAssignmentsLists : IQuery<AssignmentsListsDto>
{
    public Guid UserId { get; set; }
    public int PageSize { get; set; }
}