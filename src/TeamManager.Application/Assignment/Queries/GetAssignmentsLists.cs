using TeamManager.Application.Assignment.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Assignment.Queries;

public class GetAssignmentsLists : IQuery<AssignmentsListsDto>
{
    public Guid UserId { get; init; }
    public int PageSize { get; init; }
}