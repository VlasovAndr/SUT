using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Pages.Locators;
using WebApp;

namespace TestFramework.Pages;

public class ProductPage : BasePage
{
    private readonly ProductPageLocators locators;

    public ProductPage(IDriverFixture DriverFixture, ProductPageLocators locators) : base(DriverFixture)
    {
        this.locators = locators;
    }

    public void FillProductFields(Product product)
    {
        Driver.FindElement(locators.ProductName).ClearAndEnterText(product.Name);
        Driver.FindElement(locators.ProductDescription).ClearAndEnterText(product.Description);
        Driver.FindElement(locators.ProductPrice).ClearAndEnterText(product.Price.ToString());
        Driver.FindElement(locators.ProductType).SelectFromDropDownByText(product.ProductType.ToString());
    }

    public void ClickCreate()
    {
        Driver.FindElement(locators.CreateBtn).Click();
    }

    public Product GetProductDetails()
    {
        return new Product()
        {
            Name = Driver.FindElement(locators.ProductName).Text,
            Description = Driver.FindElement(locators.ProductDescription).Text,
            Price = int.Parse(Driver.FindElement(locators.ProductPrice).Text),
            ProductType = (ProductType)Enum.Parse(typeof(ProductType),
                            Driver.FindElement(locators.ProductType).GetAttribute("innerText").ToString())
        };
    }

    public void ClickSave()
    {
        Driver.FindElement(locators.SaveBtn).Click();
    }

    public void ClickDelete()
    {
        Driver.FindElement(locators.DeleteBtn).Click();
    }
}
