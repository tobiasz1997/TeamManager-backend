using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Assignment.Models;
using TeamManager.Core.User.Models;

namespace TeamManager.Infrastructure.DAL;

internal sealed class TeamManagerDbContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }

    public TeamManagerDbContext(DbContextOptions<TeamManagerDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}