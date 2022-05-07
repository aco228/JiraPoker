using JiraPoker.Core.Domain.Authentification.Models;

namespace JiraPoker.Core.Domain.Sessions;

public class PokerSession : IPokerSession
{
    public string Id { get; private set; }
    public string Organization { get; private set; }
    public ApplicationUserLoginModel Creator { get; private set; }

    public PokerSession(ApplicationUserLoginModel user)
    {
        Creator = user;
        Organization = user.Organization;
        Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}