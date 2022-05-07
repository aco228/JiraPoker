using Microsoft.AspNetCore.Components;

namespace JiraPoker.Core.Infrastructure.Presentation;

public abstract class PresentationPageBase<T> : ComponentBase
    where T : IPresentationLayer
{
    [Inject]
    protected T Presentation { get; set; }
    
    public abstract void OnReady();


    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            OnReady();
        }
        return base.OnAfterRenderAsync(firstRender);
    }
}