using Microsoft.Extensions.Hosting;
using Serilog;

namespace TeamManger.Common.Extensions.Serilog;

public static class SerilogExtension
{
    public static IHostBuilder AddSerilog(this IHostBuilder host)
    {
        host.UseSerilog((hostingContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

        return host;
    }
}