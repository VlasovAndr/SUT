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
    private readonly ProductListingPage productListing;
    private readonly ProductDetailsPage productDetails;

    public ProductWebSteps(ScenarioContext scenarioContext, ProductListingPage productListing, ProductDetailsPage productDetails)
    {
        this.scenarioContext = scenarioContext;
        this.productListing = productListing;
        this.productDetails = productDetails;
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

    [Then(@"I validate product is removed from the system")]
    public void ValidateProductIsRemovedFromTheSystem()
    {
        ValidateProductIsRemovedFromTheTable();
    }

    #endregion


    #region Low level action

    [When(@"I open product menu")]
    public void OpenProductMenu()
    {
        productListing.Header.OpenProductMenu();
    }

    [When(@"I click create new product")]
    public void ClickCreateProduct()
    {
        productListing.ClickCreateProduct();
    }

    [When(@"I fill product fields with following details")]
    public void FillProductFieldsWithFollowingDetails(Table table)
    {
        var product = table.CreateInstance<Product>();
        productDetails.FillProductFields(product);
        scenarioContext.Set(product);
    }

    [When(@"I click create button")]
    public void ClickCreate()
    {
        productDetails.ClickCreate();
    }

    [When(@"I click delete button")]
    public void ClickDelete()
    {
        productDetails.ClickDelete();
    }

    [When(@"I click save button")]
    public void ClickSaveButton()
    {
        productDetails.ClickSave();
    }

    [When(@"I click the (.*) link of the newly created product")]
    public void ClickLinkForNewlyCreatedProduct(string operation)
    {
        var product = scenarioContext.Get<Product>();
        productListing.PerformClickOnSpecialValue(product.Name, operation);
    }

    [Then(@"I see all the product details are created as expected")]
    public void ValidateAllProductDetailsAreCreatedAsExpected()
    {
        var product = scenarioContext.Get<Product>();
        var actualProduct = productDetails.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }

    [Then(@"I validate product is removed from the table")]
    public void ValidateProductIsRemovedFromTheTable()
    {
        var product = scenarioContext.Get<Product>();
        var isProductPresented = productListing.IsProductPresentedInTable(product.Name);

        isProductPresented.Should().BeFalse();
    }

    #endregion

}
