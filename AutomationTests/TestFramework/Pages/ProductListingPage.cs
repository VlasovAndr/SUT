using TestFramework.Driver;
using TestFramework.Pages.Locators;

namespace TestFramework.Pages;

public class ProductListingPage : BasePage
{
    private readonly ProductListingLocators locators;
    private readonly Header header;

    public Header Header => header;

    public ProductListingPage(IDriverFixture driverFixture, ProductListingLocators locators, Header header) : base(driverFixture)
    {
        this.locators = locators;
        this.header = header;
    }

    public void ClickCreateProduct()
    {
        Driver.FindElement(locators.CreateProductLink).Click();
    }

    public void PerformClickOnSpecialValue(string itemName, string operation)
    {
        var columnIndex = GetColumnIndexByName("Name");
        Driver
            .FindElement(locators.ProductOperatonForItem(itemName, columnIndex, operation))
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