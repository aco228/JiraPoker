using JiraPoker.Core.Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Infrastructure.Configurations;

public static class ConfigurationServiceExtension
{
    public static void RegisterConfigurationsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IApplicationSettingsProvider, ApplicationSettingsProvider>();
        serviceCollection.AddSingleton<IEnvironmentProvider, EnvironmentProvider>();
        serviceCollection.AddSingleton<IConfigurationProvider, ConfigurationProvider>();
    }
}