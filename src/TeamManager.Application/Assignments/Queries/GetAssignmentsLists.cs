using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignmentsLists : IQuery<AssignmentsListsDto>
{
    public Guid UserId { get; init; }
    public int PageSize { get; init; }
}