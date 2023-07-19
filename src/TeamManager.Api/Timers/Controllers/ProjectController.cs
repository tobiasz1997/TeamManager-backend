using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Timers.Requests;
using TeamManager.Application.Timers.Commands;
using TeamManager.Application.Timers.DTO;
using TeamManager.Application.Timers.Queries;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Api.Timers.Controllers;

[ApiController]
[Authorize]
[Route("project")]
public class ProjectController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("list")]
    [SwaggerOperation("Get all projects.")]
    [ProducesResponseType(typeof(IEnumerable<ProjectDto>), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetProjectsList()
    {
        return Ok(await _mediator.Send(new GetProjectsList { UserId = UserId }));
    }
    
    [HttpPost]
    [SwaggerOperation("Create project.")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status405MethodNotAllowed)]
    public async Task<ActionResult> CreateProject(CreateProjectRequest command)
    {
        var newId = Guid.NewGuid();
        await _mediator.Send(new CreateProject(newId, UserId, command.Label, command.Color));
        return Ok(newId);
    }
    
    [HttpPut]
    [SwaggerOperation("Update project.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status405MethodNotAllowed)]
    public async Task<ActionResult> UpdateProject(UpdateProjectRequest command)
    {
        await _mediator.Send(new UpdateProject(command.Id, command.Label, command.Color));
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Delete project.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        await _mediator.Send(new DeleteProject(id));
        return Ok();
    }
}