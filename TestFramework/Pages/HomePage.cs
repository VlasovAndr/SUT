using OpenQA.Selenium;
using TestFramework.Driver;

namespace TestFramework.Pages;

public class HomePage : IHomePage
{
    private readonly IWebDriver driver;

    public HomePage(IDriverFixture driverFixture) => driver = driverFixture.Driver;

    IWebElement lnkProduct => driver.FindElement(By.LinkText("Product"));
    IWebElement lnkCreate => driver.FindElement(By.LinkText("Create"));

    public void CreateProduct()
    {
        lnkProduct.Click();
        lnkCreate.Click();
    }
}