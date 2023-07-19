﻿using Mediator;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Core.Assignments.Repositories;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.Queries.Handlers;

public sealed class GetAssignmentsListsHandler : IRequestHandler<GetAssignmentsLists, AssignmentsListsDto>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentsListsHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<AssignmentsListsDto> Handle(GetAssignmentsLists request, CancellationToken cancellationToken)
    {
        var assignments = await _assignmentRepository.GetAllAsync();
        var filteredAssignments = assignments.Where(x => x.UserId == new Id(request.UserId)).OrderBy(x => x.StartDate).Select(x => new AssignmentDto()
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
            Todo = new PagedResult<AssignmentDto>(todoAssignments.Take(request.PageSize), todoAssignments.Count()),
            InProgress = new PagedResult<AssignmentDto>(inProgressAssignments.Take(request.PageSize), inProgressAssignments.Count()),
            Done = new PagedResult<AssignmentDto>(doneAssignments.Take(request.PageSize), doneAssignments.Count()),
            Aborted = new PagedResult<AssignmentDto>(abortedAssignments.Take(request.PageSize), abortedAssignments.Count()),
        };

        return results;
    }
}