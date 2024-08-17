using System.Linq;
using FluentAssertions;
using ProductAPI.Repository;
using Reqnroll;
using Reqnroll.Assist;
using TestFramework.Extensions;
using WebApp;

namespace TestProjectBDD.StepDefinitions;

[Binding, Scope(Tag = "Database_Steps")]
public class ProductDbSteps
{
    private readonly ScenarioContext scenarioContext;
    private readonly IProductRepository productRepository;

    public ProductDbSteps(ScenarioContext scenarioContext, IProductRepository productRepository)
    {
        this.scenarioContext = scenarioContext;
        this.productRepository = productRepository;
    }

    [Given(@"I create product with following details")]
    [When(@"I create product with following details")]
    public void CreateProductWithFollowingDetails(Table table)
    {
        var products = table.CreateSet<Product>();

        foreach (var product in products)
        {
            productRepository.AddProduct(product.Cast<ProductAPI.Data.Product>());
            scenarioContext.Set(product);
        }
    }

    [When(@"I edit newly created product with following details")]
    public void EditProductWithFollowingDetails(Table table)
    {
        var editProduct = table.CreateInstance<Product>();
        var product = scenarioContext.Get<Product>();
        var productFromDb = productRepository.GetProductByName(product.Name);

        productFromDb.Name = editProduct.Name;
        productFromDb.Price = editProduct.Price;
        productFromDb.Description = editProduct.Description;
        productFromDb.ProductType = (ProductAPI.Data.ProductType)editProduct.ProductType;

        productRepository.UpdateProduct(productFromDb);
        scenarioContext.Set(productFromDb.Cast<Product>);   
    }

    [When(@"I delete newly created product")]
    public void DeleteNewlyCreatedProduct()
    {
        var product = scenarioContext.Get<Product>();
        productRepository.DeleteProduct(product.Name);
    }

    [Then(@"I validate all the product details are created as expected")]
    public void ValidateProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productRepository.GetProductByName(product.Name);

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }

    [Then(@"I validate product is removed from the system")]
    public void ValidateProductIsRemovedFromTheSystem()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productRepository.GetProductByName(product.Name);

        actualProduct
            .Should().BeNull();
    }

}
