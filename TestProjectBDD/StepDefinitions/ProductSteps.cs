using FluentAssertions;
using ProductAPI.Data;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestFramework.Pages;

namespace TestProjectBDD.StepDefinitions;

[Binding]
public class ProductSteps
{
    private readonly ScenarioContext scenarioContext;
    private readonly IHomePage homePage;
    private readonly IProductPage productPage;

    public ProductSteps(ScenarioContext scenarioContext, IHomePage homePage, IProductPage productPage)
    {
        this.scenarioContext = scenarioContext;
        this.homePage = homePage;
        this.productPage = productPage;
    }

    [Given("I click the Product menu")]
    public void GivenIClickTheProductMenu()
    {
        homePage.ClickProduct();
    }

    [Given(@"I click the ""([^""]*)"" link")]
    public void GivenIClickTheLink(string link)
    {
        homePage.ClickCreate();
    }

    [Given("I create product with following details")]
    public void GivenICreateProductWithFollowingDetails(Table dataTable)
    {
        var product = dataTable.CreateInstance<Product>();
        productPage.EnterProductDetails(product);
        scenarioContext.Set(product);
    }

    [When("I click the Details link of the newly created product")]
    public void WhenIClickTheDetailsLinkOfTheNewlyCreatedProduct()
    {
        var product = scenarioContext.Get<Product>();
        homePage.PerformClickOnSpecialValue(product.Name, "Details");
    }

    [Then("I see all the product details are created as expected")]
    public void ThenISeeAllTheProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }
}
