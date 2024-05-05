using AutoFixture.Xunit2;
using FluentAssertions;
using TestFramework.Pages;
using WebApp;

namespace TestProject;

public class ProductTests
{
    private readonly HomePage homePage;
    private readonly ProductPage productPage;

    public ProductTests(HomePage homePage, ProductPage productPage)
    {
        this.homePage = homePage;
        this.productPage = productPage;
    }

    [Theory, AutoData]
    public void CreateProductTest(Product product)
    {
        homePage.CreateProduct();
        productPage.FillProductFields(product);
        productPage.ClickCreate();
        homePage.PerformClickOnSpecialValue(product.Name, "Details");

        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }
}