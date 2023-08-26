using TeamManager.Application.Assignments.DTO;
using TeamManager.Core.Assignments.Models;

namespace TeamManager.Application.Assignments.Mappers;

public static class AssignmentMapper
{
    public static AssignmentDto AsDto(this Assignment entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Priority = entity.Priority,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
}