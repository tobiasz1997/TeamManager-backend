using System.ComponentModel.DataAnnotations;

namespace TeamManager.Application.Timers.DTO;

public class ProjectDto
{
    [property: Required]
    public Guid Id { get; set; }
    [property: Required]
    public string Label { get; set; } = string.Empty;
    [property: Required]
    public string Color { get; set; } = string.Empty;
}