using Microsoft.Extensions.Configuration;
using TeamManager.Infrastructure;

namespace TeamManager.Tests.Integration;

public class OptionsProvider
{
    private readonly IConfigurationRoot _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);
    
    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables()
            .Build();
}