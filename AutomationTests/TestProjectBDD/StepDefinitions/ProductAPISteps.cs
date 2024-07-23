using FluentAssertions;
using System.Linq;
using Reqnroll;
using Reqnroll.Assist;
using WebApp;
using WebApp.Producer;

namespace TestProjectBDD.StepDefinitions;

[Binding, Scope(Tag = "API_Steps")]
public class ProductAPISteps
{
    private readonly IProductService productService;
    private readonly ScenarioContext scenarioContext;

    public ProductAPISteps(IProductService productService, ScenarioContext scenarioContext)
    {
        this.productService = productService;
        this.scenarioContext = scenarioContext;
    }

    [Given(@"I create product with following details")]
    [When(@"I create product with following details")]
    public void CreateProductWithFollowingDetails(Table table)
    {
        var products = table.CreateSet<Product>();

        foreach (var product in products)
        {
            productService.CreateProduct(product).GetAwaiter().GetResult();
            scenarioContext.Set(product);
        }
    }

    [When(@"I edit newly created product with following details")]
    public void EditProductWithFollowingDetails(Table table)
    {
        var productForEdit = table.CreateInstance<Product>();
        var product = scenarioContext.Get<Product>();
        var item = productService.GetProducts().Result.First(x => x.Name == product.Name);
        productForEdit.Id = item.Id;
        productService.EditProduct(productForEdit).GetAwaiter().GetResult();
        scenarioContext.Set(productForEdit);
    }

    [When(@"I delete newly created product")]
    public void DeleteProductWithFollowingDetails()
    {
        var product = scenarioContext.Get<Product>();
        var productForDelete = productService.GetProducts().Result.First(x => x.Name == product.Name);
        productService.DeleteProduct(productForDelete.Id).GetAwaiter().GetResult();
    }

    [Then(@"I validate all the product details are created as expected")]
    public void ValidateProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productService.GetProducts().Result.First(x => x.Name == product.Name);

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }

    [Then(@"I validate product is removed from the table")]
    public void ValidateProductIsRemovedFromTheTable()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productService.GetProducts().Result.FirstOrDefault(x => x.Name == product.Name);

        actualProduct
            .Should().BeNull();
    }
}
