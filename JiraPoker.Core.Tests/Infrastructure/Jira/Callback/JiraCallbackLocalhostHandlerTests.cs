using System;
using JiraPoker.Core.Infrastructure.Jira.Callback;
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

    private JiraCallbackLocalhostHandler GetService()
    {
        var service = new JiraCallbackLocalhostHandler();
        service.SetLocalTestDebug(false);
        return service;
    }
}