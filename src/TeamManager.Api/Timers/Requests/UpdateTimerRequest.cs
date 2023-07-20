using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Timers.Requests;

public class UpdateTimerRequest
{
    [property: Required] 
    public Guid Id { get; init; }
    [property: Required] 
    public string Description { get; init; } = string.Empty;

    [property: Required] 
    public Guid? ProjectId { get; init; }
    [property: Required] 
    public DateTime Date { get; init; }
}