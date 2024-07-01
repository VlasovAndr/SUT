using TestFramework.Driver;
using TestFramework.Pages.Locators;

namespace TestFramework.Pages;

public class HomePage : BasePage
{
    private readonly HomePageLocators locators;

    public HomePage(IDriverFixture driverFixture, HomePageLocators locators) : base(driverFixture)
    {
        this.locators = locators;
    }

    public void CreateProduct()
    {
        Driver.FindElement(locators.ProductLink).Click();
        Driver.FindElement(locators.CreateProductLink).Click();
    }

    public void OpenProductMenu()
    {
        Driver.FindElement(locators.ProductLink).Click();
    }

    public void ClickCreateProduct()
    {
        Driver.FindElement(locators.CreateProductLink).Click();
    }

    public void PerformClickOnSpecialValue(string itemName, string operation)
    {
        var columnIndex = GetColumnIndexByName("Name");
        Driver
        .FindElement(locators.ProductOperaton(itemName, columnIndex, operation))
        .Click();
    }

    public bool IsProductPresentedInTable(string name)
    {
        var columnIndex = GetColumnIndexByName("Name");
        var element = Driver.FindElements(locators.ProductInTable(name, columnIndex));

        return element.Count == 1;
    }

    private int GetColumnIndexByName(string columnName)
    {
        var columns = Driver.FindElements(locators.ColumnsInProductTable).ToList();
        return columns.FindIndex(x => x.Text == columnName) + 1;
    }

    private int GetRowIndexByColumnNameAndValue(int columnIndex, string value)
    {
        var cellValues = Driver.FindElements(locators.CellValuesInProductTable(columnIndex)).ToList();
        return cellValues.FindIndex(x => x.Text == value) + 1;
    }
}