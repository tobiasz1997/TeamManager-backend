using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Assignment.Requests;
using TeamManager.Application.Assignment.Commands;
using TeamManager.Application.Assignment.DTO;
using TeamManager.Application.Assignment.Queries;
using TeamManager.Application.Shared.Abstractions.Browsing;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Application.Shared.Abstractions.Exceptions;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Core.User.Enums;

namespace TeamManager.Api.Assignment.Controllers;

[ApiController]
[Authorize]
[Route("assignment")]
public class AssignmentController : ControllerBase
{
    private readonly IQueryHandler<GetAssignment, AssignmentDto> _getAssignment;
    private readonly IQueryHandler<GetAssignmentsList, PagedResult<AssignmentDto>> _getAssignmentsList;
    private readonly IQueryHandler<GetAssignmentsLists, AssignmentsListsDto> _getAssignmentsLists;
    private readonly ICommandHandler<CreateAssignment> _createAssignment;
    private readonly ICommandHandler<DeleteAssignment> _deleteAssignment;
    private readonly ICommandHandler<UpdateAssignment> _updateAssignment;
    private readonly ICommandHandler<UpdateAssignmentStatus> _updateAssignmentStatus;


    public AssignmentController(
        IQueryHandler<GetAssignment, AssignmentDto> getAssignment, 
        IQueryHandler<GetAssignmentsList, PagedResult<AssignmentDto>> getAssignmentsList,
        IQueryHandler<GetAssignmentsLists, AssignmentsListsDto> getAssignmentsLists,
        ICommandHandler<CreateAssignment> createAssignment,
        ICommandHandler<DeleteAssignment> deleteAssignment,
        ICommandHandler<UpdateAssignment> updateAssignment,
        ICommandHandler<UpdateAssignmentStatus> updateAssignmentStatus)
    {
        _getAssignment = getAssignment;
        _getAssignmentsList = getAssignmentsList;
        _getAssignmentsLists = getAssignmentsLists;
        _createAssignment = createAssignment;
        _deleteAssignment = deleteAssignment;
        _updateAssignment = updateAssignment;
        _updateAssignmentStatus = updateAssignmentStatus;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get assignment.")]
    [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentById(Guid id)
    {
        return Ok(await _getAssignment.HandleAsync(new GetAssignment {Id = id}));
    }
    
    [HttpGet("list")]
    [SwaggerOperation("Get assignments list.")]
    [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentsList([FromQuery] AssignmentStatusType type, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            return NotFound();
        }
        
        var userId = Guid.Parse(HttpContext.User.Identity.Name);
        return Ok(await _getAssignmentsList.HandleAsync(new GetAssignmentsList {UserId = userId, Page = page, PageSize = pageSize, Type = type}));
    }
    
    [HttpGet("lists")]
    [SwaggerOperation("Get typed lists of assignments.")]
    [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentsLists([FromQuery] int pageSize = 2)
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            return NotFound();
        }
        
        var userId = Guid.Parse(HttpContext.User.Identity.Name);
        return Ok(await _getAssignmentsLists.HandleAsync(new GetAssignmentsLists {UserId = userId, PageSize = pageSize}));
    }
    
    [HttpPost]
    [SwaggerOperation("Create assignment.")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateAssignment(CreateAssignmentRequest command)
    {
        if (string.IsNullOrWhiteSpace(HttpContext.User.Identity?.Name))
        {
            return NotFound();
        }
        
        var userId = Guid.Parse(HttpContext.User.Identity.Name);
        var newId = Guid.NewGuid();
        await _createAssignment.HandleAsync(new CreateAssignment(newId, userId, command.Name, command.Description, command.Priority, command.Status));
        return Ok(newId);
    }
    
    [HttpPut]
    [SwaggerOperation("Update assignment.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAssignment(UpdateAssignmentRequest command)
    {
        await _updateAssignment.HandleAsync(new UpdateAssignment(command.Id, command.Name, command.Description, command.Priority));
        return Ok();
    }

    [HttpPatch]
    [SwaggerOperation("Update assignment status.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAssignmentStatus(UpdateAssignmentStatus command)
    {
        await _updateAssignmentStatus.HandleAsync(new UpdateAssignmentStatus(command.Id, command.Status));
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Delete assignment.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAssignment(Guid id)
    {
        await _deleteAssignment.HandleAsync(new DeleteAssignment(id));
        return Ok();
    }
}