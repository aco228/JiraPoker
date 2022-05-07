using System;
using JiraPoker.Core.Domain.Configurations;
using JiraPoker.Core.Infrastructure.Jira.Callback;
using Moq;
using Xunit;

namespace JiraPoker.Core.Tests.Infrastructure.Jira.Callback;

public class JiraCallbackLocalhostHandlerTests
{
    [Fact]
    public void Should_Return_True_When_State_Contains_Word_Localhost()
    {
        var service = GetService();
        Assert.True(service.ShouldRedirectToLocalhost("localhost_1234"));
    }
    
    [Fact]
    public void Should_Return_False_If_We_Are_Debug()
    {
        var service = GetService();
        service.SetLocalTestDebug(true);
        
        Assert.False(service.ShouldRedirectToLocalhost("localhost_1234"));
    }

    [Fact]
    public void Should_Return_False_If_State_Does_Not_Contain_Localhost()
    {
        var service = GetService();
        Assert.False(service.ShouldRedirectToLocalhost("stateBound"));
    }

    [Fact]
    public void Should_Return_Correct_RedirectUrl_For_Localhost()
    {
        var service = GetService();
        var redirectUrl = service.GetRedirectLocalhostUrl("localhost_124");
        Assert.Equal("https://localhost:124", redirectUrl);
    }

    [Fact]
    public void Should_Thow_Exception_When_State_Is_In_Wrong_Format()
    {
        var service = GetService();
        Assert.Throws<ArgumentException>(() => { service.GetRedirectLocalhostUrl("localhost%20"); });
    }

    [Fact]
    public void Should_Return_Redirect_Url_For_Localhost_With_Correct_State_Query()
    {
        var (service, configurationManager) = GetServiceWithConfigurationManager();
        service.SetLocalTestDebug(true);
        var baseUrl = "https://url.com?state=[STATE]&scope=[SCOPES]";
        
        configurationManager.Setup(x => x.GetValue<string>(It.IsAny<string>())).Returns(baseUrl);
        configurationManager.Setup(x => x.GetSectionValue<string[]>(It.IsAny<string>()))
            .Returns(new string[] { "a", "b" });

        var expectedResult = baseUrl
            .Replace("[STATE]", "localhost_123")
            .Replace("[SCOPES]", "a%20b");

        var result = service.GetRedirectUrl(123.ToString());
        Assert.Equal(expectedResult, result);
    }
    
    [Fact]
    public void Should_Return_Redirect_Url_Ignoring_Localhost_If_This_Is_Production()
    {
        var (service, configurationManager) = GetServiceWithConfigurationManager();
        service.SetLocalTestDebug(false);
        var baseUrl = "https://url.com?state=[STATE]&scope=[SCOPES]";
        
        configurationManager.Setup(x => x.GetValue<string>(It.IsAny<string>())).Returns(baseUrl);
        configurationManager.Setup(x => x.GetSectionValue<string[]>(It.IsAny<string>()))
            .Returns(new string[] { "a", "b" });

        var expectedResult = baseUrl
            .Replace("[STATE]", "stateBound")
            .Replace("[SCOPES]", "a%20b");

        var result = service.GetRedirectUrl(123.ToString());
        Assert.Equal(expectedResult, result);
    }

    private Tuple<JiraCallbackLocalhostHandler, Mock<IConfigurationProvider>> GetServiceWithConfigurationManager()
    {
        var configurationManager = new Mock<IConfigurationProvider>();
        var service = new JiraCallbackLocalhostHandler(configurationManager.Object);
        service.SetLocalTestDebug(false);
        return new (service, configurationManager);
    }

    private JiraCallbackLocalhostHandler GetService()
    {
        var (service, _) = GetServiceWithConfigurationManager();
        return service;
    }
}