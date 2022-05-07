using JiraPoker.Core.Domain;
using JiraPoker.Core.Domain.Configurations;

namespace JiraPoker.Core.Infrastructure.Jira.Callback;

public class JiraCallbackLocalhostHandler : IJIraCallbackLocalhostHandler
{
    private readonly IConfigurationProvider _configurationProvider;

    public JiraCallbackLocalhostHandler(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }
    
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

    public string GetRedirectUrl(string port)
    {
        var scopes = _configurationProvider.GetSectionValue<string[]>("JiraScopes");
        var scopesString = scopes.Length == 1 
            ? scopes[0].Replace(":", "%3A") 
            : string.Join("%20", scopes).Replace(":", "%3A"); // TODO: Refactor

        var state = IsDebug ? $"localhost_{port}" : "stateBound";
        
        return _configurationProvider.GetValue<string>(Constants.Configuration.JiraAuthenticationUrl)
            .Replace("[SCOPES]", scopesString)
            .Replace("[STATE]", state);
    }

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