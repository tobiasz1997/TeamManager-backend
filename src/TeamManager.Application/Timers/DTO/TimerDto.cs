using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.Timers.DTO;

public class TimerDto
{
    [property: Required]
    public Guid Id { get; set; }
    public ProjectDto? Project { get; set; }
    [property: Required]
    public string Description { get; set; }
    [property: Required]
    public DateTime Date { get; set; }
}