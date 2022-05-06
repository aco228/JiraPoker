using Newtonsoft.Json;

namespace JiraPoker.Core.Domain.Jira.Authorization.Models;

public record JiraAuthorizationResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }
    
    [JsonProperty("scope")]
    public string Scope { get; set; }
}