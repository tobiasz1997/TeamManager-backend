using System.ComponentModel.DataAnnotations;
using TeamManager.Core.User.Enums;

namespace TeamManager.Application.Assignment.DTO;

public class AssignmentDto
{
    [property: Required]
    public Guid Id { get; set; }
    [property: Required]
    public string Name { get;  set; }
    [property: Required]
    public string Description { get;  set; }
    [property: Required]
    public int Priority { get;  set; }
    [property: Required]
    public AssignmentStatusType Status { get;  set; }
    [property: Required]
    public DateTime StartDate { get;  set; }
}