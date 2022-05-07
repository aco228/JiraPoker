using JiraPoker.Core.Application.LoginUser;
using JiraPoker.Core.Infrastructure.Jira.Callback;
using Microsoft.AspNetCore.Mvc;

namespace JiraPoker.Controllers;

public class JiraController : Controller
{
    private static readonly string QUERY_CODE = "code";
    private readonly ILoginUserService _loginUserService;
    private readonly IJIraCallbackLocalhostHandler _jiraLocalhostCallbackHandler;

    public JiraController(
        ILoginUserService loginUserService,
        IJIraCallbackLocalhostHandler localhostHandler)
    {
        _jiraLocalhostCallbackHandler = localhostHandler;
        _loginUserService = loginUserService;
    }
    
    public async Task<IActionResult> Callback()
    {
        try
        {
            var state = Request.Query["state"];
            if (_jiraLocalhostCallbackHandler.IsDebug &&
                _jiraLocalhostCallbackHandler.ShouldRedirectToLocalhost(state))
                return Redirect(_jiraLocalhostCallbackHandler.GetRedirectLocalhostUrl(state));
            
            if (await _loginUserService.Login(Request.Query[QUERY_CODE]))
                return Redirect("/");
            
            Response.StatusCode = 400;
            return Content($"Could not login");
        }
        catch (Exception ex)
        {
            Response.StatusCode = 400;
            return Content($"Exception {ex}");
        }
    }
}