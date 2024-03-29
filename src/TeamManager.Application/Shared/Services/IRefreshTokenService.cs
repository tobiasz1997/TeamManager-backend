﻿using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Models;

namespace TeamManager.Application.Shared.Services;

public interface IRefreshTokenService
{
    RefreshToken Create(Id userId);

    RefreshToken Refresh(RefreshToken token);
}