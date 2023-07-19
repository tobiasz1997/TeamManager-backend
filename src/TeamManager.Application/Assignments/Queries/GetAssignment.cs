using Mediator;
using TeamManager.Application.Assignments.DTO;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignment : IRequest<AssignmentDto>
{
    public Guid Id { get; init; }
}