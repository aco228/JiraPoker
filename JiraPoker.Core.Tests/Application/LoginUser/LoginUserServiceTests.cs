using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiraPoker.Core.Application.LoginUser;
using JiraPoker.Core.Domain.Authentification;
using JiraPoker.Core.Domain.Authentification.Models;
using JiraPoker.Core.Domain.Configurations;
using JiraPoker.Core.Domain.Jira.Authorization;
using JiraPoker.Core.Domain.Jira.Authorization.Models;
using Moq;
using Xunit;

namespace JiraPoker.Core.Tests.Application.LoginUser;

public class LoginUserServiceTests
{

    [Fact]
    public async Task Should_Throw_Error_When_Code_Is_Empty()
    {
        var (_, _, _, service) = GetService();
        await Assert.ThrowsAsync<ArgumentException>(async () => { await service.Login(null); });
    }

    [Fact]
    public async Task Should_Have_Correct_Order_Of_Calls()
    {
        var (authService, configurationProvider, jiraService, service) = GetService();

        configurationProvider.Setup(x => x.GetValue<string>(It.Is<string>((s) => s.Equals("JiraClientId")))).Returns("JiraClientId");
        configurationProvider.Setup(x => x.GetValue<string>(It.Is<string>((s) => s.Equals("JiraClientSecret")))).Returns("JiraClientSecret");
        
        var result = await service.Login("code");
        
        configurationProvider.Verify(x => x.GetValue<string>(It.IsAny<string>()), Times.Exactly(2));
        jiraService.Verify(x => x.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        jiraService.Verify(x => x.GetResources(), Times.Once);
        jiraService.Verify(x => x.GetInformationsAboutUser(), Times.Once);
        Assert.False(result); // will return false as there is no mocks for jira calls
    }
    
    
    [Fact]
    public async Task Should_Send_Correct_Login_Data()
    {
        var (authService, configurationProvider, jiraService, service) = GetService();

        var tokenResponse = new JiraAuthorizationResponse
        {
            AccessToken = "accessToken",
            ExpiresIn = "12",
            Scope = "scope"
        };

        var resources = new List<JiraAccessibleResourceResponse>()
        {
            new()
            {
                Id = "id",
                Name = "name",
                Url = "url",
                Scopes = new string[] {"scope1", "scope2"},
                AvatarUrl = "url"
            }
        };

        var aboutMe = new JiraAboutMeResponse()
        {
            AccountId = "accountId",
            Email = "email",
            Name = "Name",
            Picture = "picture"
        };

        authService.Setup(x => x.Login(It.IsAny<ApplicationUserLoginModel>())).ReturnsAsync(true);
        configurationProvider.Setup(x => x.GetValue<string>(It.Is<string>((s) => s.Equals("JiraClientId")))).Returns("JiraClientId");
        configurationProvider.Setup(x => x.GetValue<string>(It.Is<string>((s) => s.Equals("JiraClientSecret")))).Returns("JiraClientSecret");
        jiraService.Setup(x => x.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(tokenResponse);
        jiraService.Setup(x => x.GetResources()).ReturnsAsync(resources.ToArray);
        jiraService.Setup(x => x.GetInformationsAboutUser()).ReturnsAsync(aboutMe);
        
        var result = await service.Login("code");
        
        configurationProvider.Verify(x => x.GetValue<string>(It.IsAny<string>()), Times.Exactly(2));
        jiraService.Verify(x => x.GetToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        jiraService.Verify(x => x.GetResources(), Times.Once);
        jiraService.Verify(x => x.GetInformationsAboutUser(), Times.Once);


        authService.Verify(x => x.Login(It.IsAny<ApplicationUserLoginModel>()), Times.Once);
        
        Assert.True(result);
    }
    

    private Tuple<
            Mock<IAuthService>, 
            Mock<IConfigurationProvider>, 
            Mock<IJiraAuthorizationClient>, 
            LoginUserService> GetService()
    {
        var authService = new Mock<IAuthService>();
        var configurationProvider = new Mock<IConfigurationProvider>();
        var jira = new Mock<IJiraAuthorizationClient>();
        var service = new LoginUserService(authService.Object, configurationProvider.Object, jira.Object);
        return new (authService, configurationProvider, jira, service);
    }
}