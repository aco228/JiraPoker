using Newtonsoft.Json;

namespace JiraPoker.Core.Domain.Jira.Authorization.Models;

public record JiraAboutMeResponse
{
    [JsonProperty("account_id")]
    public string AccountId { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("picture")]
    public string Picture { get; set; }
}