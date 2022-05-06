using JiraPoker.Core.Application.LoginUser;
using Microsoft.AspNetCore.Mvc;

namespace JiraPoker.Controllers;

public class JiraController : Controller
{
    private static readonly string QUERY_CODE = "code";
    private readonly ILoginUserService _loginUserService;

    public JiraController(ILoginUserService loginUserService)
    {
        _loginUserService = loginUserService;
    }
    
    public async Task<IActionResult> Callback()
    {
        try
        {
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