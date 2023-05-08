using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Shared.Abstractions.Browsing;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Core.Assignment.Repositories;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Enums;

namespace TeamManager.Application.Assignment.Queries.Handlers;

public sealed class GetAssignmentsListsHandler : IQueryHandler<GetAssignmentsLists, AssignmentsListsDto>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentsListsHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task<AssignmentsListsDto> HandleAsync(GetAssignmentsLists query)
    {
        var assignments = await _assignmentRepository.GetAllAsync();
        var filteredAssignments = assignments.Where(x => x.UserId == new Id(query.UserId)).OrderBy(x => x.StartDate).Select(x => new AssignmentDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Priority = x.Priority,
            Status = x.Status,
            StartDate = x.StartDate
        }).ToList();

        var todoAssignments =
            filteredAssignments.Where(x => x.Status == AssignmentStatusType.ToDo).ToList();
        var inProgressAssignments =
            filteredAssignments.Where(x => x.Status == AssignmentStatusType.InProgress).ToList();
        var doneAssignments =
            filteredAssignments.Where(x => x.Status == AssignmentStatusType.Done).ToList();
        var abortedAssignments =
            filteredAssignments.Where(x => x.Status == AssignmentStatusType.Aborted).ToList();

        var results = new AssignmentsListsDto
        {
            Todo = new PagedResult<AssignmentDto>(todoAssignments.Take(query.PageSize), todoAssignments.Count()),
            InProgress = new PagedResult<AssignmentDto>(inProgressAssignments.Take(query.PageSize), inProgressAssignments.Count()),
            Done = new PagedResult<AssignmentDto>(doneAssignments.Take(query.PageSize), doneAssignments.Count()),
            Aborted = new PagedResult<AssignmentDto>(abortedAssignments.Take(query.PageSize), abortedAssignments.Count()),
        };

        return results;
    }
}