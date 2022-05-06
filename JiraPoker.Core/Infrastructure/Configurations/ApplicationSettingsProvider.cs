using JiraPoker.Core.Domain.Configurations;
using Microsoft.Extensions.Configuration;

namespace JiraPoker.Core.Infrastructure.Configurations;

public class ApplicationSettingsProvider : IApplicationSettingsProvider
{
    private readonly IConfiguration _configuration;

    public ApplicationSettingsProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    IConfiguration IApplicationSettingsProvider.Configuration => _configuration;

    public Tuple<bool, T?> GetValue<T>(string key)
    {
        var exists = _configuration.GetSection(key).Exists();
        var result = default(T);
        
        if (exists)
        {
            result = _configuration.GetValue<T>(key);
        }
        
        return new Tuple<bool, T>(exists, result);
    }
}