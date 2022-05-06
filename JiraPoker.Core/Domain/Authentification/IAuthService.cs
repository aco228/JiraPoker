using JiraPoker.Core.Domain.Authentification.Models;

namespace JiraPoker.Core.Domain.Authentification;

public interface IAuthService
{
    Task<bool> Logout();
    Task<bool> Login(ApplicationUserLoginModel jiraCode);
}