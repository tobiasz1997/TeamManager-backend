using System.ComponentModel.DataAnnotations;

namespace TeamManager.Api.Timers.Requests;

public class CreateProjectRequest
{
    [property: Required] 
    public string Label { get; init; } = string.Empty;

    [property: Required] 
    public string Color { get; init; } = string.Empty;
}