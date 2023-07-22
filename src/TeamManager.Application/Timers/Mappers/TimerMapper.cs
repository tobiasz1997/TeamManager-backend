using TeamManager.Application.Timers.DTO;
using TeamManager.Core.Timers.Models;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Application.Timers.Mappers;

public static class TimerMapper
{
    public static TimerDto AsDto(this Timer entity, IEnumerable<Project> projectsEntities)
        => new()
        {
            Id = entity.Id,
            Project = projectsEntities.SingleOrDefault(x => x.Id.Value == entity.ProjectId)?.AsDto(),
            Description = entity.Description,
            Date = entity.Date
        };
}