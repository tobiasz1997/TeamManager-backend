using Mediator;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Application.Assignments.Exceptions;
using TeamManager.Core.Assignments.Repositories;

namespace TeamManager.Application.Assignments.Queries.Handlers;

public sealed class GetAssignmentHandler : IRequestHandler<GetAssignment, AssignmentDto>
{
    private readonly IAssignmentRepositoryQueries _assignmentRepository;
    
    public GetAssignmentHandler(IAssignmentRepositoryQueries assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async ValueTask<AssignmentDto> Handle(GetAssignment request, CancellationToken cancellationToken)
    {
        var result = await _assignmentRepository.GetAsync(request.Id);
        
        if (result is null)
        {
            throw new AssignmentNotFoundException(request.Id);
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