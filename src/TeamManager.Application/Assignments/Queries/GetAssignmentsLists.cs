using Mediator;
using TeamManager.Application.Assignments.DTO;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignmentsLists : IRequest<AssignmentsListsDto>
{
    public Guid UserId { get; init; }
    public int PageSize { get; init; }
}