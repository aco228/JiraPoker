namespace JiraPoker.Core.Application.LoginUser;

public interface ILoginUserService
{
    public Task<bool> Login(string code);
}