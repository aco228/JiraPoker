namespace JiraPoker.Core.Infrastructure.Jira.Callback;

public class JiraCallbackLocalhostHandler : IJIraCallbackLocalhostHandler
{
    private bool? _localSetDebugOption = null;
    
    public bool IsDebug
    {
        get
        {
            if (_localSetDebugOption.HasValue)
                return _localSetDebugOption.Value;
            
            #if DEBUG
            return true;
            #endif
            return false;
        }
    }

    public void SetLocalTestDebug(bool value)
        => _localSetDebugOption = value;

    public bool ShouldRedirectToLocalhost(string state)
    {
        if (string.IsNullOrEmpty(state))
            throw new ArgumentException("State is null or empty");
        
        return !IsDebug && state.Contains("localhost"); // we will use state localhost with port for debug purposes;
    }

    public string GetRedirectLocalhostUrl(string state)
    {
        if (string.IsNullOrEmpty(state))
            throw new ArgumentException("State is null or empty");
        
        var split = state.Split('_');
        if (split.Length != 2)
            throw new ArgumentException("Split is not in correct format");
        return $"https://localhost:{split[1]}";
    }
}