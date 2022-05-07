using JiraPoker.Core.Domain.Authentification.Models;

namespace JiraPoker.Core.Domain.Sessions;

public interface IPokerSession
{
    public string Id { get; }
    public string Organization { get; }
    public ApplicationUserLoginModel Creator { get; }
}