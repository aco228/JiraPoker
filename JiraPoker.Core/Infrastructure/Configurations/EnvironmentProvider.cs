using JiraPoker.Core.Domain.Configurations;

namespace JiraPoker.Core.Infrastructure.Configurations;

public class EnvironmentProvider : IEnvironmentProvider
{
    public string? GetValue(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }
}