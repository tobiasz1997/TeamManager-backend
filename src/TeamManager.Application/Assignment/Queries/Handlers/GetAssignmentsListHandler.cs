﻿using TeamManager.Application.Assignment.DTO;
using TeamManager.Common.Core.Browsing;
using TeamManager.Common.MediatR.Queries;
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

    public async Task<PagedResult<AssignmentDto>> Handle(GetAssignmentsList request, CancellationToken cancellationToken)
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