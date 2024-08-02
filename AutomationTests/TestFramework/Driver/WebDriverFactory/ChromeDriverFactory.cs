using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using TestFramework.Settings;

namespace TestFramework.Driver.WebDriverFactory;

public class ChromeDriverFactory : IBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public ExecutionType Type => ExecutionType.Local;

    public IWebDriver Create()
    {
        var options = new ChromeOptions();
        options.AddArgument("--disable-gpu");
        options.AddArgument("disable-popup-blocking");
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
        options.AddArgument("disable-notifications");
        options.AddUserProfilePreference("autofill.profile_enabled", false);
        options.PageLoadStrategy = PageLoadStrategy.Eager;

        var webDriver = new ChromeDriver(options);
        webDriver.Manage().Window.Size = new Size(1920, 1080);

        return webDriver;
    }
}
