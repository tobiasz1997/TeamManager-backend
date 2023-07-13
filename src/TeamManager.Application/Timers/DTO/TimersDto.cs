using System.ComponentModel.DataAnnotations;
using TeamManager.Core.Timers.Models;

namespace TeamManager.Application.Timers.DTO;

public class TimersDto
{
    [property: Required]
    public Guid Id { get; set; }
    public Project? Project { get; set; }
    [property: Required]
    public string Description { get; set; }
    [property: Required]
    public DateTime Date { get; set; }
}