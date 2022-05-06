using JiraPoker.Core.Domain.Jira.Authorization.Models;

namespace JiraPoker.Core.Domain.Jira.Authorization;

public interface IJiraAuthorizationClient
{
    public Task<JiraAuthorizationResponse> GetToken(string secret, string clientId, string code);
    public Task<JiraAccessibleResourceResponse[]> GetResources();
    public Task<JiraAboutMeResponse> GetInformationsAboutUser();
}