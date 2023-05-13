using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace TeamManager.Tests.Integration;

internal sealed class TeamManagerTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public TeamManagerTestApp(Action<IServiceCollection> services = null)
    {
        Client = WithWebHostBuilder(builder =>
        {
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            
            builder.UseEnvironment("test");
        }).CreateClient();
    }
}