using OpenQA.Selenium;
using ProductAPI.Data;
using TestFramework.Driver;
using TestFramework.Extensions;

namespace TestFramework.Pages;

public class CreateProductPage : ICreateProductPage
{
    private readonly IWebDriver driver;

    public CreateProductPage(IDriverFixture driver)
    {
        this.driver = driver.Driver;
    }

    IWebElement txtName => driver.FindElement(By.Id("Name"));
    IWebElement txtDescription => driver.FindElement(By.Id("Description"));
    IWebElement txtPrice => driver.FindElement(By.Id("Price"));
    IWebElement ddlProductType => driver.FindElement(By.Id("ProductType"));
    IWebElement btnCreate => driver.FindElement(By.Id("Create"));

    public void EnterProductDetails(Product product)
    {
        txtName.SendKeys(product.Name);
        txtDescription.SendKeys(product.Description);
        txtPrice.SendKeys(product.Price.ToString());
        ddlProductType.SelectFromDropDownByText(product.ProductType.ToString());
        btnCreate.Click();
    }
}