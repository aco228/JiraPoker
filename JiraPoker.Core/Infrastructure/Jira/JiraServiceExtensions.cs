using JiraPoker.Core.Domain.Jira.Authorization;
using JiraPoker.Core.Infrastructure.Jira.Authorization;
using JiraPoker.Core.Infrastructure.Jira.Callback;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Infrastructure.Jira;

public static class JiraServiceExtensions
{
    public static void AddJiraServices(this IServiceCollection service)
    {
        service.AddSingleton<IJIraCallbackLocalhostHandler, JiraCallbackLocalhostHandler>();
        service.AddTransient<IJiraAuthorizationClient, JiraAuthorizationClient>();
    }
}