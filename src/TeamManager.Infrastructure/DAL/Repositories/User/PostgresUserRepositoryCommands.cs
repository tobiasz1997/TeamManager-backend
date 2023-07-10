using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.User.Models;
using TeamManager.Core.User.Repositories;
using TeamManager.Core.User.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Repositories;

internal sealed class PostgresUserRepositoryCommands : IUserRepositoryCommands
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresUserRepositoryCommands(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(Id id) => _dbContext.User
        .SingleOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(Email email) => _dbContext.User
        .SingleOrDefaultAsync(x => x.Email == email);
    
    public Task AddAsync(User user)
    {
        _dbContext.User.AddAsync(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user)
    {
        _dbContext.User.Update(user);
        return Task.CompletedTask;
    }
}