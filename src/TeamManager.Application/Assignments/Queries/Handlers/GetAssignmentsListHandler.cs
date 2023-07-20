using Mediator;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Application.Assignments.Mappers;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Assignments.Repositories;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Application.Assignments.Queries.Handlers;

public sealed class GetAssignmentsListHandler : IRequestHandler<GetAssignmentsList, PagedResult<AssignmentDto>>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentsListHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<PagedResult<AssignmentDto>> Handle(GetAssignmentsList request, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository
            .GetAllAsync(request.UserId, request.Type);
        var mappedAssignments = assignments
            .Select(x => x.AsDto())
            .ToList();

        var results = new PagedResult<AssignmentDto>(mappedAssignments.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize), mappedAssignments.Count());

        return results;
    }
}