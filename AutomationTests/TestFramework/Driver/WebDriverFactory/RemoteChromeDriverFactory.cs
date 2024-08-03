using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Drawing;
using TestFramework.Settings;

namespace TestFramework.Driver.WebDriverFactory;

public class RemoteChromeDriverFactory : IBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public ExecutionType Type => ExecutionType.Remote;

    private readonly TestSettings testSettings;

    public RemoteChromeDriverFactory(TestSettings testSettings)
    {
        this.testSettings = testSettings;
    }

    public IWebDriver Create()
    {
        var options = new ChromeOptions();
        options.AddAdditionalOption("se:recordVideo", true);

        var webDriver = new RemoteWebDriver(testSettings.SeleniumGridUrl, options);
        webDriver.Manage().Window.Size = new Size(1920, 1080);

        return webDriver;
    }
}
