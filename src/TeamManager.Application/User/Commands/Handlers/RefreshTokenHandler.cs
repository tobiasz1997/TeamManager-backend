using MediatR;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.User.DTO;
using TeamManager.Application.User.Exceptions;
using TeamManager.Common.MediatR.Commands;
using TeamManager.Core.User.Repositories;

namespace TeamManager.Application.User.Commands.Handlers;

internal sealed class RefreshTokenHandler : ICommandHandler<RefreshToken>
{
    private readonly IClock _clock;
    private readonly IJwtService _jwtService;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepositoryCommands _refreshTokenRepository;
    private readonly IUserRepositoryQueries _userRepository;

    public RefreshTokenHandler(
        IAccessTokenStorage tokenStorage, 
        IRefreshTokenService refreshTokenService, 
        IRefreshTokenRepositoryCommands refreshTokenRepository, 
        IClock clock,
        IJwtService jwtService, 
        IUserRepositoryQueries userRepository)
    {
        _tokenStorage = tokenStorage;
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _clock = clock;
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RefreshToken request, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenRepository.GetByToken(request.Token);

        if (token is null || token.ExpiresAt < _clock.Current())
        {
            throw new AuthenticationException();
        }

        var user = await _userRepository.GetByIdAsync(token.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(token.UserId);
        }

        var accessToken = _jwtService.CreateToken(user.Id, user.Email);
        token = _refreshTokenService.Refresh(token);
        await _refreshTokenRepository.Update(token);

        _tokenStorage.Set(new AuthResultDto() {AccessToken = accessToken, RefreshToken = token.Token});

        return Unit.Value;
    }
}