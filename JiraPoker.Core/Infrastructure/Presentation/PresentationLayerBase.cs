namespace JiraPoker.Core.Infrastructure.Presentation;

public abstract class PresentationLayerBase : IPresentationLayer
{
    public event IPresentationLayer.OnStateChanges? StateHasChanges;
    
    internal void TriggerStateHasChanged() => StateHasChanges?.Invoke();
}