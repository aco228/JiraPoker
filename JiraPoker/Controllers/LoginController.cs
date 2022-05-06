using JiraPoker.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using IConfigurationProvider = JiraPoker.Core.Domain.Configurations.IConfigurationProvider;

namespace JiraPoker.Controllers;

public class LoginController : Controller
{
    private readonly IConfigurationProvider _configuration;
    
    public LoginController(IConfigurationProvider configurationProvider)
    {
        _configuration = configurationProvider;
    }
    
    public IActionResult Index()
    {
        var scopes = _configuration.GetSectionValue<string[]>("JiraScopes");
        var scopesString = scopes.Length == 1 
            ? scopes[0].Replace(":", "%3A") 
            : string.Join("%20", scopes).Replace(":", "%3A"); // TODO: Refactor
        
        ViewBag.Url = _configuration.GetValue<string>(Constants.Configuration.JiraAuthenticationUrl).Replace("[SCOPES]", scopesString);
        return View("~/Views/Login.cshtml");
    }
}