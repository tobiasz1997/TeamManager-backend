using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Timers.Requests;
using TeamManager.Application.Timers.DTO;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.Core.Browsing;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Api.Timers.Controllers;

[ApiController]
[Authorize]
[Route("timer")]
public class TimerController : BaseApiController
{
    private readonly IMediator _mediator;

    public TimerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("list")]
    [SwaggerOperation("Get timers list.")]
    [ProducesResponseType(typeof(PagedResult<TimersDto>), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTimersList([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] Guid projectId, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
    {
        //TODO: try to move queries to object
        return Ok();
    }
    
    [HttpPost]
    [SwaggerOperation("Create timer.")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CreateTimer(CreateTimerRequest command)
    {
        var newId = Guid.NewGuid();
        return Ok(newId);
    }
    
    [HttpPut]
    [SwaggerOperation("Update timer.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTimer(UpdateTimerRequest command)
    {
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Delete timer.")]
    [ProducesResponseType( StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTimer(Guid id)
    {
        return Ok();
    }
}