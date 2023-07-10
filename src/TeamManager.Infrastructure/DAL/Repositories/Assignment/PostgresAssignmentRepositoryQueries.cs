using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Assignment.Models;
using TeamManager.Core.Assignment.Repositories;
using TeamManager.Core.Shared.ValueObjects;

namespace TeamManager.Infrastructure.DAL.Repositories;

internal sealed class PostgresAssignmentRepositoryQueries : IAssignmentRepositoryQueries
{
    private readonly TeamManagerDbContext _dbContext;
    
    public PostgresAssignmentRepositoryQueries(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Assignment?> GetAsync(Id id) => _dbContext.Assignments
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Assignment>> GetAllAsync()
    {
        var results = await _dbContext.Assignments
            .AsNoTracking()
            .ToListAsync();
        return results;
    }
}