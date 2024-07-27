using FluentAssertions;
using Reqnroll;
using Reqnroll.Assist;
using TestFramework.Pages;
using WebApp;

namespace TestProjectBDD.StepDefinitions;

[Binding, Scope(Tag = "UI_Steps")]
public class ProductWebSteps
{
    private readonly ScenarioContext scenarioContext;
    private readonly HomePage homePage;
    private readonly ProductPage productPage;

    public ProductWebSteps(ScenarioContext scenarioContext, HomePage homePage, ProductPage productPage)
    {
        this.scenarioContext = scenarioContext;
        this.homePage = homePage;
        this.productPage = productPage;
    }

    #region High level action

    [Given(@"I create product with following details")]
    [When(@"I create product with following details")]
    public void CreateProductWithFollowingDetails(Table table)
    {
        OpenProductMenu();
        ClickCreateProduct();
        FillProductFieldsWithFollowingDetails(table);
        ClickCreate();
    }

    [When(@"I edit newly created product with following details")]
    public void EditProductWithFollowingDetails(Table table)
    {
        OpenProductMenu();
        ClickLinkForNewlyCreatedProduct("Edit");
        FillProductFieldsWithFollowingDetails(table);
        ClickSaveButton();
    }

    [When(@"I delete newly created product")]
    public void DeleteProduct()
    {
        OpenProductMenu();
        ClickLinkForNewlyCreatedProduct("Delete");
        ClickDelete();
    }

    [Then(@"I validate all the product details are created as expected")]
    public void ValidateProductDetailsAreCreatedAsExpected()
    {
        ClickLinkForNewlyCreatedProduct("Details");
        ValidateAllProductDetailsAreCreatedAsExpected();
    }

    #endregion


    #region Low level action

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

    [When(@"I click delete button")]
    public void ClickDelete()
    {
        productPage.ClickDelete();
    }

    [When(@"I click save button")]
    public void ClickSaveButton()
    {
        productPage.ClickSave();
    }

    [When(@"I click the (.*) link of the newly created product")]
    public void ClickLinkForNewlyCreatedProduct(string operation)
    {
        var product = scenarioContext.Get<Product>();
        homePage.PerformClickOnSpecialValue(product.Name, operation);
    }

    [Then(@"I see all the product details are created as expected")]
    public void ValidateAllProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }

    [Then(@"I validate product is removed from the table")]
    public void ValidateProductIsRemovedFromTheTable()
    {
        var product = scenarioContext.Get<Product>();
        var isProductPresented = homePage.IsProductPresentedInTable(product.Name);

        isProductPresented.Should().BeFalse();
    }

    #endregion

}
