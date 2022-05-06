namespace JiraPoker.Core.Domain.Configurations;

public interface IEnvironmentProvider
{
    public string? GetValue(string key);
}