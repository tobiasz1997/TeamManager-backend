using System.ComponentModel.DataAnnotations;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Application.Assignments.DTO;

public class AssignmentDto
{
    [property: Required]
    public Guid Id { get; set; }
    [property: Required]
    public string Name { get;  set; } = string.Empty;
    public string Description { get;  set; } = string.Empty;
    [property: Required]
    public int Priority { get;  set; }
    [property: Required]
    public AssignmentStatusType Status { get;  set; }
    [property: Required]
    public DateTime CreatedAt { get;  set; }
}