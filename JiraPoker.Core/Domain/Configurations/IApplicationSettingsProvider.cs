namespace JiraPoker.Core.Domain.Configurations;

public interface IApplicationSettingsProvider
{   
    public Tuple<bool, T?> GetValue<T>(string key);
}