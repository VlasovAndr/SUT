using ProductAPI.Repository;
using System;
using System.Linq;
using Reqnroll;
using TestFramework.Extensions;
using WebApp;

namespace TestProjectBDD.Hooks;

[Binding]
public class ProductDbHooks : BaseHooks
{
    private readonly IProductRepository productRepository;

    public ProductDbHooks(ScenarioContext scenarioContext, IProductRepository productRepository) : base(scenarioContext)
    {
        this.productRepository = productRepository;
    }

    #region Setups

    [BeforeScenario("@Setup.Db.CreateProduct")]
    public void CreateProduct()
    {
        var productName = GetParameterFromTag("ProductName");
        var description = GetParameterFromTag("Description");
        var price = int.Parse(GetParameterFromTag("Price"));
        var productType = GetParameterFromTag("ProductType");

        var productWebModel = new Product
        {
            Name = productName,
            Description = description,
            Price = price,
            ProductType = (ProductType)Enum.Parse(typeof(ProductType), productType)
        };

        productRepository.AddProduct(productWebModel.Cast<ProductAPI.Data.Product>());
        scenarioContext.Set(productWebModel);
    }

    #endregion


    #region Teardowns

    [AfterScenario("@Teardown.Db.DeleteCreatedProduct")]
    public void DeleteProduct()
    {
        var productForDelete = scenarioContext.Get<Product>();
        var products = productRepository.GetAllProducts().Where(x => x.Name == productForDelete.Name).ToList();

        foreach (var product in products)
        {
            productRepository.DeleteProduct(product.Id);
        }
    }

    #endregion
}
