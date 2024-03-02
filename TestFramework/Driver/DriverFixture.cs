using OpenQA.Selenium;
using TestFramework.Settings;

namespace TestFramework.Driver;

public class DriverFixture : IDriverFixture
{
    private IWebDriver driver;
    private readonly TestSettings settings;

    private readonly IBrowserDriver browserDriver;

    public DriverFixture(TestSettings settings, IBrowserDriver browserDriver)
    {
        this.settings = settings;
        this.browserDriver = browserDriver;
        driver = GetWebDriver();
    }

    public IWebDriver Driver => driver;

    private IWebDriver GetWebDriver()
    {
        return settings.BrowserType switch
        {
            BrowserType.Chrome => browserDriver.GetChromeDriver(),
            BrowserType.Firefox => browserDriver.GetFirefoxDriver(),
            _ => browserDriver.GetChromeDriver()
        };
    }
}

