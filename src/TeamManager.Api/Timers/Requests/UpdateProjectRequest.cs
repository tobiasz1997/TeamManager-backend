using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Timers.Requests;

public class UpdateProjectRequest
{
    [property: Required] 
    public Guid Id { get; init; }
    [property: Required] 
    public string Label { get; init; } = null!;
    [property: Required] 
    public string Color { get; init; } = null!;
}