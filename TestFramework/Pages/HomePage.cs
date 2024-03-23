using OpenQA.Selenium;
using TestFramework.Driver;

namespace TestFramework.Pages;

public class HomePage : IHomePage
{
    private readonly IWebDriver driver;

    public HomePage(IDriverFixture driverFixture) => driver = driverFixture.Driver;

    IWebElement lnkProduct => driver.FindElement(By.LinkText("Product"));
    IWebElement lnkCreate => driver.FindElement(By.LinkText("Create New"));

    public void CreateProduct()
    {
        lnkProduct.Click();
        lnkCreate.Click();
    }

    public void ClickProduct()
    {
        lnkProduct.Click();
    }

    public void ClickCreate()
    {
        lnkCreate.Click();
    }

    public void PerformClickOnSpecialValue(string itemName, string operation)
    {
        var columnIndex = GetColumnIndexByName("Name");

        driver
        .FindElement(By.XPath($"//table/tbody/tr/td[{columnIndex}][contains(text(),'{itemName}')]/..//td[6]/a[text() = '{operation}']"))
        .Click();
    }

    private int GetColumnIndexByName(string columnName)
    {
        var columns = driver.FindElements(By.XPath("//table/thead//th")).ToList();
        return columns.FindIndex(x => x.Text == columnName) + 1;
    }

    private int GetRowIndexByColumnNameAndValue(int columnIndex, string value)
    {
        var cellValues = driver.FindElements(By.XPath($"//table/tbody/tr/td[{columnIndex}]")).ToList();
        return cellValues.FindIndex(x => x.Text == value) + 1;
    }
}