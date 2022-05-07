using JiraPoker.Core.Domain.Authentification.Models;
using JiraPoker.Core.Infrastructure.Presentation;

namespace JiraPoker.Core.Presentation.HomeView;

public interface IHomeViewPresentationLayer : IPresentationLayer
{
    public ApplicationUserLoginModel CurrentUser { get; set; }
    public bool DisplayCreateNewSessionModal { get; }
    void OnCreateNewSession();
    void OnCreateNewSessionModalClose();
    void OnCreateNewSessionModalSave();
}