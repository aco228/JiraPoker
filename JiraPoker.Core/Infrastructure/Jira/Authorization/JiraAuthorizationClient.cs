using JiraPoker.Core.Domain.HttpRequests;
using JiraPoker.Core.Domain.Jira.Authorization;
using JiraPoker.Core.Domain.Jira.Authorization.Models;
using JiraPoker.Core.Infrastructure.HttpRequests;

namespace JiraPoker.Core.Infrastructure.Jira.Authorization;

public class JiraAuthorizationClient : IJiraAuthorizationClient
{
    private IRequestClient _requestClient;
    
    public JiraAuthorizationClient()
    {
        _requestClient = new RequestClient();
    }

    public async Task<JiraAuthorizationResponse> GetToken(string secret, string clientId, string code)
    {
        var request = new JiraAuthorizationRequest
        {
            GrantType = "authorization_code",
            ClientId = clientId,
            ClientSecret = secret,
            Code = code, 
            RedirectUrl = "https://jira-poker.herokuapp.com", // TODO: move to config
        };

        var response = await _requestClient.Post<JiraAuthorizationResponse>("https://auth.atlassian.com/oauth/token", request);
        
        _requestClient.AddAuthorization(response.AccessToken);
        _requestClient.AddDefaultHeader("Accept", "application/json");

        return response;
    }

    public Task<JiraAccessibleResourceResponse[]> GetResources()
        => _requestClient.Get<JiraAccessibleResourceResponse[]>("https://api.atlassian.com/oauth/token/accessible-resources");

    public Task<JiraAboutMeResponse> GetInformationsAboutUser()
        => _requestClient.Get<JiraAboutMeResponse>("https://api.atlassian.com/me");
}