using JiraPoker.Core.Domain.Authentification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Infrastructure.Authentification;

public static class AuthenticationServiceExtensions
{
    public static void AddAuthenticationServices(this IServiceCollection service)
    {
        service.AddSingleton<IAuthService, AuthService>();
        service.AddSingleton<IAuthStateProvider, AuthStateProvider>();
        service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "_auth";
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
                options.SlidingExpiration = false;
            });
    }
}