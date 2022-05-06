using Microsoft.AspNetCore.Mvc;
using IConfigurationProvider = JiraPoker.Core.Domain.Configurations.IConfigurationProvider;

namespace JiraPoker.Controllers;

public class JiraController : Controller
{
    private IConfigurationProvider _configurationProvider;

    public JiraController(IConfigurationProvider configurationProvider)
    {
        _configurationProvider = configurationProvider;
    }

    public IActionResult Test()
    {
        return Content(_configurationProvider.GetValueOrDefault("JiraAuthenticationUrl"));
    }
    
    public async Task<IActionResult> Callback()
    {
        var response = Request.QueryString
                       + Environment.NewLine
                       + Environment.NewLine
                       + await ReadBody();
                       
        return Content(response);
    }
    
    protected async Task<string> ReadBody()
    {
        var bodyStream = new StreamReader(Request.Body);
        var bodyText = await bodyStream.ReadToEndAsync();
        return bodyText;
    }
}