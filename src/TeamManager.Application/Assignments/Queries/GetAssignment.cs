using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Assignments.Queries;

public class GetAssignment : IQuery<AssignmentDto>
{
    public Guid Id { get; init; }
}