namespace JiraPoker.Core.Domain.Authentification.Models;

public class ApplicationUserLoginModel
{
    public string AccountId { get; set; }
    public string CloudId { get; set; }
    public string Organization { get; set; }
    public string OrganizationUrl { get; set; }
    public string Token { get; set; }
    
    public string Email { get; set; }
    public string PictureUrl { get; set; }
    public string Name { get; set; }
}