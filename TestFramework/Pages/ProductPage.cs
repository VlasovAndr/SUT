using OpenQA.Selenium;
using TestFramework.Driver;
using TestFramework.Extensions;
using WebApp;

namespace TestFramework.Pages;

public class ProductPage : BasePage
{
    public ProductPage(IDriverFixture DriverFixture) : base(DriverFixture)
    {
    }

    IWebElement txtName => Driver.FindElement(By.Id("Name"));
    IWebElement txtDescription => Driver.FindElement(By.Id("Description"));
    IWebElement txtPrice => Driver.FindElement(By.Id("Price"));
    IWebElement ddlProductType => Driver.FindElement(By.Id("ProductType"));
    IWebElement btnCreate => Driver.FindElement(By.Id("Create"));
    IWebElement btnSave => Driver.FindElement(By.Id("Save"));
    IWebElement btnDelete => Driver.FindElement(By.Id("Delete"));

    public void FillProductFields(Product product)
    {
        txtName.ClearAndEnterText(product.Name);
        txtDescription.ClearAndEnterText(product.Description);
        txtPrice.ClearAndEnterText(product.Price.ToString());
        ddlProductType.SelectFromDropDownByText(product.ProductType.ToString());
    }

    public void ClickCreate()
    {
        btnCreate.Click();
    }

    public Product GetProductDetails()
    {
        return new Product()
        {
            Name = txtName.Text,
            Description = txtDescription.Text,
            Price = int.Parse(txtPrice.Text),
            ProductType = (ProductType)Enum.Parse(typeof(ProductType),
                          ddlProductType.GetAttribute("innerText").ToString())
        };
    }

    public void EditProduct(Product product)
    {
        txtName.ClearAndEnterText(product.Name);
        txtDescription.ClearAndEnterText(product.Description);
        txtPrice.ClearAndEnterText(product.Price.ToString());
        ddlProductType.SelectFromDropDownByText(product.ProductType.ToString());
    }

    public void ClickSave()
    {
        btnSave.Click();
    }

    public void ClickDelete()
    {
        btnDelete.Click();
    }
}
