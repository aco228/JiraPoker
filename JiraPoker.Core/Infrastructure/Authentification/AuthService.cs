using System.Security.Claims;
using JiraPoker.Core.Domain.Authentification;
using JiraPoker.Core.Domain.Authentification.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace JiraPoker.Core.Infrastructure.Authentification;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _contextAccessor;
    
    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
    }
    
    public async Task<bool> Logout()
    {
        try
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Login(ApplicationUserLoginModel login)
    {
        var claims = new List<Claim>
        {
            new (ApplicationClaims.AccountId, login.AccountId),
            new (ApplicationClaims.CloudId, login.CloudId),
            new (ApplicationClaims.Organization, login.Organization),
            new (ApplicationClaims.OrganizationUrl, login.OrganizationUrl),
            new (ApplicationClaims.Email, login.Email),
            new (ApplicationClaims.PictureUrl, login.PictureUrl),
            new (ApplicationClaims.Name, login.Name),
            new (ApplicationClaims.Token, login.Token),
        };
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };
        
        try
        {
            await _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }
        catch (Exception ex)
        {
            var error = ex.Message;
            return false;
        }
    }
}