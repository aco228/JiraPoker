using System.Security.Claims;
using JiraPoker.Core.Domain.Authentification;
using JiraPoker.Core.Domain.Authentification.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace JiraPoker.Core.Infrastructure.Authentification;

public class AuthStateProvider : IAuthStateProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthStateProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public ApplicationUserLoginModel GetUser()
    {
        var user = _contextAccessor.HttpContext.User;
        if (user == null)
            throw new ArgumentException("No user");
        
        return new()
        {
            AccountId = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.AccountId).Value,
            CloudId = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.CloudId).Value,
            Organization = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.Organization).Value,
            OrganizationUrl = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.OrganizationUrl).Value,
            Email = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.Email).Value,
            PictureUrl = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.PictureUrl).Value,
            Name = user.Claims.FirstOrDefault(x => x.Type == ApplicationClaims.Name).Value,
        };
    }
}