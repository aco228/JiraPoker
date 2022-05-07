using JiraPoker.Core.Domain.Authentification;
using JiraPoker.Core.Domain.Authentification.Models;
using JiraPoker.Core.Domain.Sessions;
using JiraPoker.Core.Infrastructure.Presentation;

namespace JiraPoker.Core.Presentation.HomeView;

public class HomeViewPresentationLayer : PresentationLayerBase, IHomeViewPresentationLayer
{
    private readonly IAuthStateProvider _stateProvider;
    private readonly ISessionManager _sessionManager;
    
    public ApplicationUserLoginModel CurrentUser { get; set; }
    public bool DisplayCreateNewSessionModal { get; protected set; } = false;
    
    public HomeViewPresentationLayer(
        IAuthStateProvider authStateProvider,
        ISessionManager sessionManager)
    {
        _stateProvider = authStateProvider;
        _sessionManager = sessionManager;
        FindCurrentUser();
    }

    private void FindCurrentUser()
    {
        CurrentUser = _stateProvider.GetUser();
        TriggerStateHasChanged();
    }


    public void OnCreateNewSession()
    {
        DisplayCreateNewSessionModal = true;
        TriggerStateHasChanged();
    }

    public void OnCreateNewSessionModalClose()
    {
        DisplayCreateNewSessionModal = false;
        TriggerStateHasChanged();
    }

    public void OnCreateNewSessionModalSave()
    {
        DisplayCreateNewSessionModal = false;
        TriggerStateHasChanged();
    }
}