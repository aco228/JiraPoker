namespace JiraPoker.Core.Infrastructure.Jira.Callback;

public interface IJIraCallbackLocalhostHandler
{
    bool IsDebug { get; }

    /// <summary>
    /// Used for tests, avoid using it
    /// </summary>
    void SetLocalTestDebug(bool value);

    /// <summary>
    /// Get redirect url for Jira auth
    /// </summary>
    string GetRedirectUrl(string port);
    
    bool ShouldRedirectToLocalhost(string state);
    string GetRedirectLocalhostUrl(string state);

}