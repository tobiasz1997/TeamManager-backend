using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Users.Models;
using TeamManager.Core.Users.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Users;

internal sealed class PostgresUserRepositoryQueries : IUserRepositoryQueries
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresUserRepositoryQueries(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(Id id) => _dbContext.User
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);
}