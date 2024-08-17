using TestFramework.Driver;
using TestFramework.Pages.Locators;

namespace TestFramework.Pages;

public class Header : BasePage
{
    private readonly HeaderLocators locators;

    public Header(IDriverFixture driverFixture, HeaderLocators locators) : base(driverFixture)
    {
        this.locators = locators;
    }

    public void OpenProductMenu()
    {
        Driver.FindElement(locators.ProductMenu).Click();
    }

    public void OpenHomeMenu()
    {
        Driver.FindElement(locators.HomeMenu).Click();
    }
}