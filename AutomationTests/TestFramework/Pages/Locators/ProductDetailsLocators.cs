using OpenQA.Selenium;

namespace TestFramework.Pages.Locators;

public class ProductDetailsLocators
{
    public By ProductName => By.Id("Name");
    public By ProductDescription => By.Id("Description");
    public By ProductPrice => By.Id("Price");
    public By ProductType => By.Id("ProductType");
    public By CreateBtn => By.Id("Create");
    public By SaveBtn => By.Id("Save");
    public By DeleteBtn => By.Id("Delete");
}
