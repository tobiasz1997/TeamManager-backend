using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;
using TeamManager.Core.User.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories;

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