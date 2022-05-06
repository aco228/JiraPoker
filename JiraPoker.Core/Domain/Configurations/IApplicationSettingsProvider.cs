using Microsoft.Extensions.Configuration;

namespace JiraPoker.Core.Domain.Configurations;

public interface IApplicationSettingsProvider
{   
    internal IConfiguration Configuration { get; }
    public Tuple<bool, T?> GetValue<T>(string key);
}