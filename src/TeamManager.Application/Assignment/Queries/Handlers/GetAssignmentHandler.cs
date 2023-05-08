using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Assignment.Exceptions;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Core.Assignment.Repositories;

namespace TeamManager.Application.Assignment.Queries.Handlers;

public sealed class GetAssignmentHandler : IQueryHandler<GetAssignment, AssignmentDto>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }
    
    public async Task<AssignmentDto> HandleAsync(GetAssignment query)
    {
        var result = await _assignmentRepository.GetAsync(query.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(query.Id);
        }

        return new AssignmentDto()
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Priority = result.Priority,
            Status = result.Status,
            StartDate = result.StartDate
        };
    }
}