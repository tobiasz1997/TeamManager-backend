using TeamManager.Application.Timers.DTO;
using TeamManager.Core.Timers.Models;

namespace TeamManager.Application.Timers.Mappers;

public static class ProjectMapper
{
    public static ProjectDto AsDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Label = entity.Label,
            Color = entity.Color
        };
}