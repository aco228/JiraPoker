using Newtonsoft.Json;

namespace JiraPoker.Core.Domain.Jira.Authorization.Models;

public class JiraAuthorizationRequest
{
    [JsonProperty("grant_type")]
    public string GrantType { get; set; }
    
    [JsonProperty("client_id")]
    public string ClientId { get; set; }
    
    [JsonProperty("client_secret")]
    public string ClientSecret { get; set; }
    
    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("redirect_uri")]
    public string RedirectUrl { get; set; }
}