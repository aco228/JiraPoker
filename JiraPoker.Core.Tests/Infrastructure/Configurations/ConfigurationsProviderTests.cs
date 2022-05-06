using System;
using JiraPoker.Core.Domain.Configurations;
using Moq;
using Xunit;
using ConfigurationProvider = JiraPoker.Core.Infrastructure.Configurations.ConfigurationProvider;

namespace JiraPoker.Core.Tests.Infrastructure.Configurations;

public class ConfigurationsProviderTests
{
    
    [Fact]
    public void Should_Return_Expected_String_From_AppSettings()
    {
        var (appSettings, environmentProvider, provider) = GetProvider();
        var expectedValue = "aco";

        appSettings.Setup(x => x.GetValue<string>(It.IsAny<string>())).Returns(new Tuple<bool, string?>(true, expectedValue));

        var result = provider.GetValue<string>("someKey");
        Assert.Equal(expectedValue, result);
    }
    
    [Fact]
    public void Should_Return_Expected_Int_From_AppSettings()
    {
        var (appSettings, environmentProvider, provider) = GetProvider();
        var expectedValue = 25;

        appSettings.Setup(x => x.GetValue<int>(It.IsAny<string>())).Returns(new Tuple<bool, int>(true, expectedValue));

        var result = provider.GetValue<int>("someKey");
        Assert.Equal(expectedValue, result);
    }
    
    
    [Fact]
    public void Should_Return_Favor_AppSettings_Instead_Of_Environment()
    {
        var (appSettings, environmentProvider, provider) = GetProvider();
        var expectedValue = "aco";

        appSettings.Setup(x => x.GetValue<string>(It.IsAny<string>())).Returns(new Tuple<bool, string?>(true, expectedValue));
        environmentProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns("differentValue");

        var result = provider.GetValue<string>("someKey");
        Assert.Equal(expectedValue, result);
    }
    
    [Fact]
    public void Should_Return_And_Convert_Value_From_Environment_If_Not_Present_In_AppSettings()
    {
        var (appSettings, environmentProvider, provider) = GetProvider();
        var expectedValue = 5;

        appSettings.Setup(x => x.GetValue<int>(It.IsAny<string>())).Returns(new Tuple<bool, int>(false, 0));
        environmentProvider.Setup(x => x.GetValue(It.IsAny<string>())).Returns("5");

        var result = provider.GetValue<int>("someKey");
        Assert.Equal(expectedValue, result);
    }
    
    private Tuple<Mock<IApplicationSettingsProvider>, Mock<IEnvironmentProvider>, ConfigurationProvider> GetProvider()
    {
        var applicationSettings = new Mock<IApplicationSettingsProvider>();
        var enviromentProvider = new Mock<IEnvironmentProvider>();
        var provider = new ConfigurationProvider(applicationSettings.Object, enviromentProvider.Object);
        return new Tuple<Mock<IApplicationSettingsProvider>, Mock<IEnvironmentProvider>, ConfigurationProvider>(applicationSettings, enviromentProvider, provider);
    }
}