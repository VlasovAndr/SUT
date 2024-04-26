using Azure;
using OpenQA.Selenium;
using TestFramework.Driver;

namespace TestFramework.Pages;

public class HomePage : BasePage
{
    public HomePage(IDriverFixture driverFixture) : base(driverFixture)
    {
    }

    IWebElement lnkProduct => Driver.FindElement(By.LinkText("Product"));
    IWebElement lnkCreate => Driver.FindElement(By.LinkText("Create New"));

    public void CreateProduct()
    {
        lnkProduct.Click();
        lnkCreate.Click();
    }

    public void OpenProductMenu()
    {
        lnkProduct.Click();
    }

    public void ClickCreateProduct()
    {
        lnkCreate.Click();
    }

    public void PerformClickOnSpecialValue(string itemName, string operation)
    {
        var columnIndex = GetColumnIndexByName("Name");

        Driver
        .FindElement(By.XPath($"//table/tbody/tr/td[{columnIndex}][contains(text(),'{itemName}')]/..//td[6]/a[text() = '{operation}']"))
        .Click();
    }

    private int GetColumnIndexByName(string columnName)
    {
        var columns = Driver.FindElements(By.XPath("//table/thead//th")).ToList();
        return columns.FindIndex(x => x.Text == columnName) + 1;
    }

    private int GetRowIndexByColumnNameAndValue(int columnIndex, string value)
    {
        var cellValues = Driver.FindElements(By.XPath($"//table/tbody/tr/td[{columnIndex}]")).ToList();
        return cellValues.FindIndex(x => x.Text == value) + 1;
    }
}