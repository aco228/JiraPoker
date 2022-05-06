using JiraPoker.Core.Domain.Authentification.Models;

namespace JiraPoker.Core.Domain.Authentification;

public interface IAuthStateProvider
{
    ApplicationUserLoginModel GetUser();
}