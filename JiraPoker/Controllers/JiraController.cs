using Microsoft.AspNetCore.Mvc;

namespace JiraPoker.Controllers;

public class JiraController : Controller
{
    private IConfiguration _configuration;

    public JiraController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Test()
    {
        return Content(_configuration.GetSection("Jira").GetSection("Authentication").GetValue<string>("Url"));
    }

    public IActionResult Test2()
    {
        var result = Environment.GetEnvironmentVariable("test");
        if (string.IsNullOrEmpty(result))
            return Content("Null");
        return Content(result);
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