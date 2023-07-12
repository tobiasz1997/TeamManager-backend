using Microsoft.EntityFrameworkCore;
using TeamManager.Core.Assignments.Models;
using TeamManager.Core.Timers.Models;
using TeamManager.Core.Users.Models;
using Timer = TeamManager.Core.Timers.Models.Timer;

namespace TeamManager.Infrastructure.DAL;

internal sealed class TeamManagerDbContext : DbContext
{
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<RefreshToken> RefreshToken { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Timer> Timers { get; set; }

    public TeamManagerDbContext(DbContextOptions<TeamManagerDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}