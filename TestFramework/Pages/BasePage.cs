using OpenQA.Selenium;
using TestFramework.Driver;

namespace TestFramework.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver { get; }

    public BasePage(IDriverFixture driverFixture) => Driver = driverFixture.Driver;

    public void ClosePage()
    {
        Driver.Quit();
    }
}