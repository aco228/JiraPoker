namespace JiraPoker.Core.Infrastructure.Presentation;

public interface IPresentationLayer
{
    delegate void OnStateChanges();

    event OnStateChanges StateHasChanges;
}