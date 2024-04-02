using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace TestFramework.Driver;

public class BrowserDriver : IBrowserDriver
{
    public IWebDriver GetChromeDriver()
    {
        return new ChromeDriver();
    }

    public IWebDriver GetFirefoxDriver()
    {
        return new FirefoxDriver();
    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Safari,
    Edge
}