using Microsoft.Extensions.Configuration;

namespace JiraPoker.Core.Domain.Configurations;

public interface IApplicationSettingsProvider
{   
    public IConfiguration Configuration { get; }
    public Tuple<bool, T?> GetValue<T>(string key);
}