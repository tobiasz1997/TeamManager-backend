using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Browsing;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Core.Assignment.Repositories;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Application.Assignment.Queries.Handlers;

public sealed class GetAssignmentsListHandler : IQueryHandler<GetAssignmentsList, PagedResult<AssignmentDto>>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentsListHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task<PagedResult<AssignmentDto>> HandleAsync(GetAssignmentsList query)
    {
        var assignments = await _assignmentRepository.GetAllAsync();
        var filteredAssignments = assignments
            .Where(x => x.UserId == new Id(query.UserId) && x.Status == query.Type)
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

        var results = new PagedResult<AssignmentDto>(filteredAssignments.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize), filteredAssignments.Count());

        return results;
    }
}