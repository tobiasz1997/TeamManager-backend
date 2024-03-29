using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Timers.Requests;

public class CreateTimerRequest
{
    [property: Required] 
    public string Description { get; init; } = string.Empty;
    public Guid? ProjectId { get; init; }
    [property: Required] 
    public DateTime Date { get; init; }
}