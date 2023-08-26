using Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.Users.Requests;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.Users.Commands;
using TeamManager.Application.Users.DTO;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.Core.Exceptions.Abstractions;

namespace TeamManager.Api.Users.Controllers;

[ApiController]
[Route("identity")]
public class IdentityController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IAccessTokenStorage _tokenStorage;

    public IdentityController(
        IMediator mediator,
        IAccessTokenStorage tokenStorage)
    {
        _mediator = mediator;
        _tokenStorage = tokenStorage;
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up and create new account.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp([FromBody] SignUpRequest command)
    {
        await _mediator.Send(new SignUp(Guid.NewGuid(), command.Email, command.Password, command.FirstName, command.LastName));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
    
    [HttpPost("token/refresh")]
    [SwaggerOperation("Refresh token.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest command)
    {
        await _mediator.Send(new RefreshToken(command.RefreshToken));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in to user account.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest command)
    {
        await _mediator.Send(new SignIn(command.Email, command.Password));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
}