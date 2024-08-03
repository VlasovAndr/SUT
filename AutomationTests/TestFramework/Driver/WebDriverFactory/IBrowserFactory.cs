using OpenQA.Selenium;
using TestFramework.Settings;

namespace TestFramework.Driver.WebDriverFactory;

public interface IBrowserFactory
{
    BrowserName Name { get; }
    ExecutionType Type { get; }
    IWebDriver Create();
}
