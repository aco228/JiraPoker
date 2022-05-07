using JiraPoker.Core.Domain.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Infrastructure.SessionManager;

public static class SessionServiceExtensions
{
    public static void RegisterPokerSessionServices(this IServiceCollection service)
    {
        service.AddSingleton<ISessionManager, SessionManager>();
    }
}