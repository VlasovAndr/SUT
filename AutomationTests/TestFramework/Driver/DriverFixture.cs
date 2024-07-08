using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using TestFramework.Settings;

namespace TestFramework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private IWebDriver driver;
    private readonly TestSettings testSettings;
    private readonly IServiceProvider serviceProvider;

    public IWebDriver Driver => driver;

    public DriverFixture(TestSettings testSettings, IServiceProvider serviceProvider)
    {
        this.testSettings = testSettings;
        this.serviceProvider = serviceProvider;
        driver = CreateWebDriver();
        driver.Navigate().GoToUrl(testSettings.ApplicationUrl);
    }

    private IWebDriver CreateWebDriver()
    {
        var factory = serviceProvider.GetServices<IBrowserFactory>()
            .FirstOrDefault(f => f.Name == testSettings.BrowserName && f.Type == testSettings.ExecutionType);

        if (factory == null)
        {
            throw new Exception(
                $"No factory registered for BrowserName: '{testSettings.BrowserName}' and BrowserType:'{testSettings.ExecutionType}'.");
        }

        return factory.Create();
    }

    public void Dispose()
    {
        driver.Quit();
    }

}

