using Microsoft.EntityFrameworkCore;
using TeamManager.Infrastructure;
using TeamManager.Infrastructure.DAL;

namespace TeamManager.Tests.Integration;

internal class TestDatabase : IDisposable
{
    public TeamManagerDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("database");
        DbContext = new TeamManagerDbContext(new DbContextOptionsBuilder<TeamManagerDbContext>()
            .UseNpgsql(options.ConnectionString).Options);
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}