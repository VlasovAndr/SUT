using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Drawing;
using TestFramework.Settings;

namespace TestFramework.Driver.WebDriverFactory;

public class FirefoxDriverFactory : IBrowserFactory
{
    public BrowserName Name => BrowserName.Firefox;
    public ExecutionType Type => ExecutionType.Local;

    public IWebDriver Create()
    {
        var options = new FirefoxOptions();
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-popup-blocking");
        options.SetPreference("network.cookie.cookieBehavior", 0);
        options.AddArgument("disable-notifications");

        var webDriver = new FirefoxDriver(options);
        webDriver.Manage().Window.Size = new Size(1920, 1080);

        return webDriver;
    }
}
