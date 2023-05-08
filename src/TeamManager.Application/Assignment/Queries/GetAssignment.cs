using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Queries;

namespace TeamManager.Application.Assignment.Queries;

public class GetAssignment : IQuery<AssignmentDto>
{
    public Guid Id { get; set; }
}