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
    private readonly HomePage homePage;
    private readonly ProductPage productPage;

    public ProductSteps(ScenarioContext scenarioContext, HomePage homePage, ProductPage productPage)
    {
        this.scenarioContext = scenarioContext;
        this.homePage = homePage;
        this.productPage = productPage;
    }

    [Given(@"I create product with following details"), Scope(Tag = "UI")]
    public void CreateProductWithFollowingDetails(Table table)
    {
        OpenProductMenu();
        ClickCreate();
        CreateProductWithFollowingDetails(table);
        ClickCreate();
        productPage.ClosePage();
    }

    [When(@"I open product menu")]
    public void OpenProductMenu()
    {
        homePage.OpenProductMenu();
    }

    [When(@"I click create new product")]
    public void ClickCreateProduct()
    {
        homePage.ClickCreateProduct();
    }

    [When(@"I fill product fields with following details")]
    public void FillProductFieldsWithFollowingDetails(Table table)
    {
        var product = table.CreateInstance<Product>();
        productPage.FillProductFields(product);
        scenarioContext.Set(product);
    }

    [When(@"I click create button")]
    public void ClickCreate()
    {
        productPage.ClickCreate();
    }

    [When(@"I click save button")]
    public void ClickSaveButton()
    {
        productPage.ClickSave();
    }

    [When(@"I click the (.*) link of the newly created product")]
    public void ClickDetailsLinkNewlyCreatedProduct(string operation)
    {
        var product = scenarioContext.Get<Product>();
        homePage.PerformClickOnSpecialValue(product.Name, operation);
    }

    [Then(@"I see all the product details are created as expected")]
    public void ICanSeeAllProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }

    [Then(@"I validate all the product details are created as expected")]
    public void ValidateProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        ClickDetailsLinkNewlyCreatedProduct("Details");
        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }
}
