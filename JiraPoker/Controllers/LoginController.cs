using JiraPoker.Core.Infrastructure.Jira.Callback;
using Microsoft.AspNetCore.Mvc;

namespace JiraPoker.Controllers;

public class LoginController : Controller
{
    private readonly IJIraCallbackLocalhostHandler _jIraCallbackLocalhost;
    
    public LoginController(
        IJIraCallbackLocalhostHandler jIraCallbackLocalhostHandler)
    {
        _jIraCallbackLocalhost = jIraCallbackLocalhostHandler;
    }
    
    public IActionResult Index()
    {
        var port = Request.HttpContext.Connection.LocalPort;
        ViewBag.Url = _jIraCallbackLocalhost.GetRedirectUrl(port.ToString());
        return View("~/Views/Login.cshtml");
    }
}