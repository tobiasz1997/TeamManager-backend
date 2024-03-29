﻿using Mediator;
using TeamManager.Application.Shared.Services;
using TeamManager.Application.Users.DTO;
using TeamManager.Application.Users.Exceptions;
using TeamManager.Core.Users.Models;
using TeamManager.Core.Users.Repositories;
using TeamManager.Core.Users.ValueObjects;

namespace TeamManager.Application.Users.Commands.Handlers;

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

    public async ValueTask<Unit> Handle(SignUp request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmailAsync(new Email(request.Email)) is not null)
        {
            throw new EmailAlreadyInUseException(request.Email);
        }
        
        var securedPassword = _passwordService.Secure(request.Password);
        var user = new User(request.Id, request.Email, securedPassword, request.FirstName, request.LastName,
            _clock.Current());

        await _userRepository.AddAsync(user);
        
        var accessToken = _jwtService.CreateToken(user);
        var refreshToken = _refreshTokenService.Create(user.Id);

        await _refreshTokenRepository.Insert(refreshToken);
        
        _tokenStorage.Set(new AuthResultDto() { AccessToken = accessToken, RefreshToken = refreshToken.Token});

        return Unit.Value;
    }
}