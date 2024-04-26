using ProductAPI.Data;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using WebApp.Producer;

namespace TestProjectBDD.Hooks;

[Binding]
public class ProductAPIHooks : BaseHooks
{
    private readonly IProductService productService;

    public ProductAPIHooks(ScenarioContext scenarioContext, IProductService productService) : base(scenarioContext)
    {
        this.productService = productService;
    }

    #region Setups

    [BeforeScenario("@Setup.API.CreateProduct")]
    public void CreateProduct()
    {
        var productName = GetParameterFromTag("ProductName");
        var description = GetParameterFromTag("Description");
        var price = int.Parse(GetParameterFromTag("Price"));
        var productType = GetParameterFromTag("ProductType");

        var productWebModel = new WebApp.Product
        {
            Name = productName,
            Description = description,
            Price = price,
            ProductType = (WebApp.ProductType)Enum.Parse(typeof(ProductType), productType)
        };

        var productAPIModel = new Product
        {
            Name = productName,
            Description = description,
            Price = price,
            ProductType = (ProductType)Enum.Parse(typeof(ProductType), productType)
        };

        productService.CreateProduct(productWebModel);
        scenarioContext.Set(productWebModel);
        scenarioContext.Set(productAPIModel);
    }

    #endregion


    #region Teardowns

    [AfterScenario("@Teardown.API.DeleteCreatedProduct")]
    public void DeleteProduct()
    {
        var productForDelete = scenarioContext.Get<Product>();
        var products = productService.GetProducts().Result.Where(x => x.Name == productForDelete.Name);

        foreach (var product in products)
        {
            productService.DeleteProduct(product.Id);
        }

    }

    #endregion
}
