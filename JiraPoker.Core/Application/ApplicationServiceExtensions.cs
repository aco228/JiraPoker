using JiraPoker.Core.Application.LoginUser;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Application;

public static class ApplicationServiceExtensions
{
    public static void RegisterApplicationServices(this IServiceCollection service)
    {
        service.AddScoped<ILoginUserService, LoginUserService>();
    }
}