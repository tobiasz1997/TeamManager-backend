using Mediator;
using TeamManager.Application.Assignments.DTO;
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
        var assignments = await _assignmentRepository.GetAllAsync();
        var filteredAssignments = assignments
            .Where(x => x.UserId == new Id(request.UserId) && x.Status == request.Type)
            .OrderBy(x => x.StartDate)
            .Select(x => new AssignmentDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Priority = x.Priority,
                Status = x.Status,
                StartDate = x.StartDate
            }).ToList();

        var results = new PagedResult<AssignmentDto>(filteredAssignments.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize), filteredAssignments.Count());

        return results;
    }
}