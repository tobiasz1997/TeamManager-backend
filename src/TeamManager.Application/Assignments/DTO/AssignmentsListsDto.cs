using System.ComponentModel.DataAnnotations;
using TeamManager.Common.Core.Browsing;

namespace TeamManager.Application.Assignments.DTO;

public class AssignmentsListsDto
{
    [property: Required]
    public PagedResult<AssignmentDto> Todo { get; set; }
    [property: Required]
    public PagedResult<AssignmentDto> InProgress { get; set; }
    [property: Required]
    public PagedResult<AssignmentDto> Done { get; set; }
    [property: Required]
    public PagedResult<AssignmentDto> Aborted { get; set; }
}