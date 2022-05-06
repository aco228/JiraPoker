using JiraPoker.Core.Domain.Authentification.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace JiraPoker.Core.Domain.Authentification;

public interface IAuthStateProvider
{
    ApplicationUserLoginModel GetUser();
}