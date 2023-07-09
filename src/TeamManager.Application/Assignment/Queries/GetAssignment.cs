using TeamManager.Application.Assignment.DTO;
using TeamManager.Common.MediatR.Queries;

namespace TeamManager.Application.Assignment.Queries;

public class GetAssignment : IQuery<AssignmentDto>
{
    public Guid Id { get; init; }
}