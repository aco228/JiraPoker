using Newtonsoft.Json;

namespace JiraPoker.Core.Domain.Jira.Authorization.Models;

public record JiraAccessibleResourceResponse
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("scopes")]
    public string[] Scopes { get; set; }
    
    [JsonProperty("avatarUrl")]
    public string AvatarUrl { get; set; }
}