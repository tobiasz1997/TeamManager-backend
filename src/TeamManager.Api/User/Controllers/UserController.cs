using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TeamManager.Api.User.Requests;
using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Application.Shared.Abstractions.Queries;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.User.Commands;
using TeamManager.Application.User.DTO;
using TeamManager.Application.User.Queries;
using TeamManager.Common.AspNet.Controller;
using TeamManager.Common.AspNet.Exceptions.Abstractions;

namespace TeamManager.Api.User.Controllers;

[ApiController]
[Route("user")]
public class UserController : BaseApiController
{
    private readonly ICommandHandler<SignUp> _signUpCommandHandler;
    private readonly ICommandHandler<SignIn> _signInCommandHandler;
    private readonly ICommandHandler<RefreshToken> _refreshTokenCommandHandler;
    private readonly IQueryHandler<GetUser, UserDto> _getUserQueryHandler;
    private readonly IAccessTokenStorage _tokenStorage;

    public UserController(
        ICommandHandler<SignUp> signUpCommandHandler,
        IQueryHandler<GetUser, UserDto> getUserQueryHandler, 
        ICommandHandler<SignIn> signInCommandHandler, 
        IAccessTokenStorage tokenStorage, 
        ICommandHandler<RefreshToken> refreshTokenCommandHandler)
    {
        _signUpCommandHandler = signUpCommandHandler;
        _getUserQueryHandler = getUserQueryHandler;
        _signInCommandHandler = signInCommandHandler;
        _tokenStorage = tokenStorage;
        _refreshTokenCommandHandler = refreshTokenCommandHandler;
    }

    [HttpGet("me")]
    [Authorize]
    [SwaggerOperation("Get logged user profile.")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get()
    {
        return Ok(await _getUserQueryHandler.HandleAsync(new GetUser {UserId = UserId}));
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up and create new account.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp([FromBody] SignUpRequest command)
    {
        await _signUpCommandHandler.HandleAsync(new SignUp(Guid.NewGuid(), command.Email, command.Password, command.FirstName, command.LastName));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
    
    [HttpPost("token/refresh")]
    [SwaggerOperation("Refresh token.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest command)
    {
        await _refreshTokenCommandHandler.HandleAsync(new RefreshToken(command.RefreshToken));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
    
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in to user account.")]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignIn([FromBody] SignInRequest command)
    {
        await _signInCommandHandler.HandleAsync(new SignIn(command.Email, command.Password));
        var result = _tokenStorage.Get();
        return Ok(result);
    }
}