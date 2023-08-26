using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Users.Requests;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.Users.Commands;
using TeamManager.Application.Users.DTO;
using TeamManager.Application.Users.Queries;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Api.Users.Controllers;

[ApiController]
[Authorize]
[Route("user")]
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    [Authorize]
    [SwaggerOperation("Get logged user profile.")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        return Ok(await _mediator.Send(new GetUser {UserId = UserId}));
    }
}