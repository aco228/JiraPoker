using JiraPoker.Core.Domain.Authentification.Models;

namespace JiraPoker.Core.Domain.Sessions;

public interface ISessionManager
{
    IEnumerable<IPokerSession> GetOrganizationSessions(string organization);
    IPokerSession TryCreateNewSession(ApplicationUserLoginModel user);
    bool TryRemoveSession(IPokerSession session);
}