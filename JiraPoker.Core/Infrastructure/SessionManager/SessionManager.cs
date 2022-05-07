using System.Collections.Concurrent;
using JiraPoker.Core.Domain.Authentification.Models;
using JiraPoker.Core.Domain.Sessions;

namespace JiraPoker.Core.Infrastructure.SessionManager;

public class SessionManager : ISessionManager
{
    protected ConcurrentDictionary<string, IPokerSession> _sessions = new();

    public IEnumerable<IPokerSession> GetOrganizationSessions(string organization)
    {
        foreach (var session in _sessions)
            if (session.Value.Organization.Equals(organization))
                yield return session.Value;
    }

    public IPokerSession TryCreateNewSession(ApplicationUserLoginModel user)
    {
        var session = new PokerSession(user);
        if (!_sessions.TryAdd(session.Id, session))
            return null;

        return session;
    }

    public bool TryRemoveSession(IPokerSession session)
    {
        if (!_sessions.ContainsKey(session.Id))
            return false;

        if (_sessions.TryRemove(new (session.Id, session)))
            return false;

        return true;
    }
}