﻿using Mediator;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.Users.DTO;
using TeamManager.Application.Users.Exceptions;
using TeamManager.Core.Users.Repositories;

namespace TeamManager.Application.Users.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUserRepositoryCommands _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IAccessTokenStorage _tokenStorage;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IRefreshTokenRepositoryCommands _refreshTokenRepository;

    public SignInHandler(
        IUserRepositoryCommands userRepository,
        IPasswordService passwordService, 
        IAccessTokenStorage tokenStorage, 
        IRefreshTokenService refreshTokenService, 
        IRefreshTokenRepositoryCommands refreshTokenRepository, 
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenStorage = tokenStorage;
        _refreshTokenService = refreshTokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
    }

    public async ValueTask<Unit> Handle(SignIn request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordService.Validate(request.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        var accessToken = _jwtService.CreateToken(user);
        var token = await _refreshTokenRepository.GetByUserId(user.Id);

        if (token is null)
        {
            token = _refreshTokenService.Create(user.Id);
            await _refreshTokenRepository.Insert(token);
        }
        else
        {
            token = _refreshTokenService.Refresh(token);
            await _refreshTokenRepository.Update(token);
        }

        _tokenStorage.Set(new AuthResultDto() {AccessToken = accessToken, RefreshToken = token.Token});

        return Unit.Value;
    }
}