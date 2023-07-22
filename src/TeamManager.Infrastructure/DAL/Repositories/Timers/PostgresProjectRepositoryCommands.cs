using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Shared.ValueObjects;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Timers.Repositories;

namespace TeamManager.Infrastructure.DAL.Repositories.Timers;

internal sealed class PostgresProjectRepositoryCommands : IProjectRepositoryCommand
{
    private readonly TeamManagerDbContext _dbContext;

    public PostgresProjectRepositoryCommands(TeamManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Project?> GetAsync(Id id) => _dbContext.Projects
        .SingleOrDefaultAsync(x => x.Id == id);

    public Task CreateAsync(Project project)
    {
        _dbContext.Projects.AddAsync(project);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Project project)
    {
        _dbContext.Projects.Update(project);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Project project)
    {
        _dbContext.Projects.Remove(project);
        return Task.CompletedTask;
    }
}