using JiraPoker.Core.Presentation.HomeView;
using JiraPoker.Core.Presentation.SessionView;
using Microsoft.Extensions.DependencyInjection;

namespace JiraPoker.Core.Presentation;

public static class PresentationLayerServiceExtension
{
    public static void RegisterPresentationLayers(this IServiceCollection service)
    {
        service.AddScoped<IHomeViewPresentationLayer, HomeViewPresentationLayer>();
        service.AddScoped<ISessionViewPresentationLayer, SessionViewPresentationLayer>();
    }
}