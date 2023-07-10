using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;
using TeamManager.Core.User.Repositories;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Repositories;

internal sealed class PostgresRefreshTokenRepositoryCommands : IRefreshTokenRepositoryCommands
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresRefreshTokenRepositoryCommands(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<RefreshToken?> GetByToken(Token token) =>
        _dbContext.RefreshToken.SingleOrDefaultAsync(x => x.Token == token);

    public Task<RefreshToken?> GetByUserId(Id userId) => _dbContext.RefreshToken.SingleOrDefaultAsync(x => x.UserId == userId);

    public Task Insert(RefreshToken refreshToken)
    {
        _dbContext.RefreshToken.AddAsync(refreshToken);
        return Task.CompletedTask;
    }

    public async Task<bool> IsInUse(Id id, Id userId, Token token)
    {
        var value = await _dbContext.RefreshToken.SingleAsync(x => x.Id != id && (x.UserId == userId || x.Token == token));
        return value == null;
    }

    public Task Update(RefreshToken token)
    {
        _dbContext.RefreshToken.Update(token);
        return Task.CompletedTask;
    }
}