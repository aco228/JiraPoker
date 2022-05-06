using Microsoft.AspNetCore.Mvc;

namespace JiraPoker.Controllers;

public class JiraController : Controller
{
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