using TeamManager.Application.Shared.Abstractions.Commands;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.User.DTO;
using TeamManager.Application.User.Exceptions;
using TeamManager.Core.User.Repositories;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Application.User.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IClock _clock;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepositoryCommands _refreshTokenRepository;
    private readonly IUserRepositoryCommands _userRepository;
    private readonly IAccessTokenStorage _tokenStorage;

    public SignUpHandler(IClock clock, IPasswordService passwordService, IUserRepositoryCommands userRepository, IJwtService jwtService, IAccessTokenStorage tokenStorage, IRefreshTokenService refreshTokenService, IRefreshTokenRepositoryCommands refreshTokenRepository)
    {
        _clock = clock;
        _passwordService = passwordService;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _tokenStorage = tokenStorage;
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public async Task HandleAsync(SignUp command)
    {
        if (await _userRepository.GetByEmailAsync(new Email(command.Email)) is not null)
        {
            throw new EmailAlreadyInUseException(command.Email);
        }
        
        var securedPassword = _passwordService.Secure(command.Password);
        var user = new Core.User.Models.User(command.Id, command.Email, securedPassword, command.FirstName, command.LastName,
            _clock.Current());

        await _userRepository.AddAsync(user);
        
        var accessToken = _jwtService.CreateToken(user.Id, user.Email);
        var refreshToken = _refreshTokenService.Create(user.Id);

        await _refreshTokenRepository.Insert(refreshToken);
        
        _tokenStorage.Set(new AuthResultDto() { AccessToken = accessToken, RefreshToken = refreshToken.Token});
    }
}