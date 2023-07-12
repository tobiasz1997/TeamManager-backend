using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Assignments.Requests;
using TeamManager.Application.Assignments.Commands;
using TeamManager.Application.Assignments.DTO;
using TeamManager.Application.Assignments.Queries;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.Core.Browsing;
using TeamManager.Common.Core.Exceptions.Abstractions;
using TeamManager.Core.Users.Enums;

namespace TeamManager.Api.Assignments.Controllers;

[ApiController]
[Authorize]
[Route("assignment")]
public class AssignmentController : BaseApiController
{
    private readonly IMediator _mediator;

    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id:guid}")]
    [SwaggerOperation("Get assignment.")]
    [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentById(Guid id)
    {
        return Ok(await _mediator.Send(new GetAssignment {Id = id}));
    }
    
    [HttpGet("list")]
    [SwaggerOperation("Get assignments list.")]
    [ProducesResponseType(typeof(PagedResult<AssignmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentsList([FromQuery] AssignmentStatusType type, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
    {
        return Ok(await _mediator.Send(new GetAssignmentsList {UserId = UserId, Page = page, PageSize = pageSize, Type = type}));
    }
    
    [HttpGet("lists")]
    [SwaggerOperation("Get typed lists of assignments.")]
    [ProducesResponseType(typeof(AssignmentsListsDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAssignmentsLists([FromQuery] int pageSize = 2)
    {
        return Ok(await _mediator.Send(new GetAssignmentsLists {UserId = UserId, PageSize = pageSize}));
    }
    
    [HttpPost]
    [SwaggerOperation("Create assignment.")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateAssignment(CreateAssignmentRequest command)
    {
        var newId = Guid.NewGuid();
        await _mediator.Send(new CreateAssignment(newId, UserId, command.Name, command.Description, command.Priority, command.Status));
        return Ok(newId);
    }
    
    [HttpPut]
    [SwaggerOperation("Update assignment.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAssignment(UpdateAssignmentRequest command)
    {
        await _mediator.Send(new UpdateAssignment(command.Id, command.Name, command.Description, command.Priority));
        return Ok();
    }

    [HttpPatch]
    [SwaggerOperation("Update assignment status.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAssignmentStatus(UpdateAssignmentStatus command)
    {
        await _mediator.Send(new UpdateAssignmentStatus(command.Id, command.Status));
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Delete assignment.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAssignment(Guid id)
    {
        await _mediator.Send(new DeleteAssignment(id));
        return Ok();
    }
}