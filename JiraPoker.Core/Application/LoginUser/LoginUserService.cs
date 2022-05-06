using JiraPoker.Core.Domain;
using JiraPoker.Core.Domain.Authentification;
using JiraPoker.Core.Domain.Configurations;
using JiraPoker.Core.Domain.Jira.Authorization;

namespace JiraPoker.Core.Application.LoginUser;

public class LoginUserService : ILoginUserService
{
    private readonly IAuthService _authService;
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IJiraAuthorizationClient _jiraAuthorization;
    
    public LoginUserService(
        IAuthService authService,
        IConfigurationProvider configurationProvider,
        IJiraAuthorizationClient jiraAuthorizationClient)
    {
        _authService = authService;
        _configurationProvider = configurationProvider;
        _jiraAuthorization = jiraAuthorizationClient;
    }
    
    public async Task<bool> Login(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentException("Code is null");
        
        var jiraClientId = _configurationProvider.GetValue<string>(Constants.Configuration.JiraClientId);
        var jiraSecret = _configurationProvider.GetValue<string>(Constants.Configuration.JiraClientSecret);

        try
        {
            var jiraToken = await _jiraAuthorization.GetToken(jiraSecret, jiraClientId, code);
            var resources = await _jiraAuthorization.GetResources();
            var userInformations = await _jiraAuthorization.GetInformationsAboutUser();
            
            return await _authService.Login(new ()
            {
                AccountId = userInformations.AccountId,
                CloudId = resources.FirstOrDefault().Id,
                Organization = resources.FirstOrDefault().Name,
                OrganizationUrl = resources.FirstOrDefault().Url,
                Email = userInformations.Email,
                PictureUrl = userInformations.Picture,
                Name = userInformations.Name,
                Token = jiraToken.AccessToken,
            });;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}