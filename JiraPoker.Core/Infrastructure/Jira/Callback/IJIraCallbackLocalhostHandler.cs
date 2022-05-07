namespace JiraPoker.Core.Infrastructure.Jira.Callback;

public interface IJIraCallbackLocalhostHandler
{
    bool IsDebug { get; }

    /// <summary>
    /// Used for tests, avoid using it
    /// </summary>
    /// <param name="value"></param>
    void SetLocalTestDebug(bool value);
    bool ShouldRedirectToLocalhost(string state);
    string GetRedirectLocalhostUrl(string state);

}