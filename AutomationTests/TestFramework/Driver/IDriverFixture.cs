using OpenQA.Selenium;

namespace TestFramework.Driver;

public interface IDriverFixture
{
    IWebDriver Driver { get; }
    string GetBrowserLogs();
    byte[] GetScreenshot();
}

