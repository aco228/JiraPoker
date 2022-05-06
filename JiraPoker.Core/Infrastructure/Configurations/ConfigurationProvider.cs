using System.ComponentModel;
using JiraPoker.Core.Domain.Configurations;
using Microsoft.Extensions.Configuration;
using IConfigurationProvider = JiraPoker.Core.Domain.Configurations.IConfigurationProvider;

namespace JiraPoker.Core.Infrastructure.Configurations;

public class ConfigurationProvider : IConfigurationProvider
{
    private readonly IApplicationSettingsProvider _applicationSettings;
    private readonly IEnvironmentProvider _environmentProvider;
    
    public ConfigurationProvider(
        IApplicationSettingsProvider applicationSettingsProvider,
        IEnvironmentProvider environmentProvider)
    {
        _applicationSettings = applicationSettingsProvider;
        _environmentProvider = environmentProvider;
    }
    
    public T? GetValue<T>(string key)
    {
        var (configurationExists, configurationValue) = _applicationSettings.GetValue<T?>(key);
        if (configurationExists)
            return configurationValue;

        var environmentValue = _environmentProvider.GetValue(key);
        if (string.IsNullOrEmpty(environmentValue))
            return default(T);
            
        var converter = TypeDescriptor.GetConverter(typeof(T));

        try
        {
            return (T) converter.ConvertFromString(environmentValue)!;
        }
        catch
        {
            return default(T);
        }
    }

    public string GetValueOrDefault(string key)
    {
        var value = GetValue<string>(key);
        return string.IsNullOrEmpty(value) ? string.Empty : value;
    }
}