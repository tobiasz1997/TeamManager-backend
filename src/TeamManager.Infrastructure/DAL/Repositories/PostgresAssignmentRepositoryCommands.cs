using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Assignment.Models;
using TeamManager.Core.Assignment.Repositories;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Repositories;

internal sealed class PostgresAssignmentRepositoryCommands : IAssignmentRepositoryCommands
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresAssignmentRepositoryCommands(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Assignment?> GetAsync(Id id) => _dbContext.Assignments
        .SingleOrDefaultAsync(x => x.Id == id);

    public Task CreateAsync(Assignment assignment)
    {
        _dbContext.Assignments.AddAsync(assignment);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Assignment assignment)
    {
        _dbContext.Assignments.Update(assignment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Assignment assignment)
    {
        _dbContext.Assignments.Remove(assignment);
        return Task.CompletedTask;
    }
}