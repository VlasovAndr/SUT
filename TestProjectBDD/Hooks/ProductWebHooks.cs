using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductAPI.Data;
using System;
using TechTalk.SpecFlow;
using TestProjectBDD.StepDefinitions;
using WebApp.Producer;

namespace TestProjectBDD.Hooks;

[Binding]
public class ProductWebHooks : BaseHooks
{
    private readonly ProductSteps productSteps;

    public ProductWebHooks(ScenarioContext scenarioContext, ProductSteps productSteps) : base(scenarioContext)
    {
        this.productSteps = productSteps;
    }

    #region Setups

    [BeforeScenario("@Setup.UI.CreateProduct")]
    public void CreateProduct()
    {
        var productName = GetParameterFromTag("ProductName");
        var description = GetParameterFromTag("Description");
        var price = GetParameterFromTag("Price");
        var productType = GetParameterFromTag("ProductType");
        var productTable = new TechTalk.SpecFlow.Table("Name", "Description", "Price", "ProductType");
        productTable.AddRow(productName, description, price, productType);

        productSteps.CreateProductWithFollowingDetails(productTable);
    }

    #endregion


    #region Teardowns
    [AfterScenario("@Teardown.UI.DeleteCreatedProduct")]
    public void DeleteProduct()
    {
        productSteps.DeleteProduct();
    }
    #endregion
}
