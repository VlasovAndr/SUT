using AutoFixture.Xunit2;
using FluentAssertions;
using ProductAPI.Data;
using TestFramework.Pages;

namespace TestProject;

public class ProductTests
{
    private readonly IHomePage homePage;
    private readonly IProductPage productPage;

    public ProductTests(IHomePage homePage, IProductPage productPage)
    {
        this.homePage = homePage;
        this.productPage = productPage;
    }

    [Theory, AutoData]
    public void CreateProductTest(Product product)
    {
        homePage.CreateProduct();
        productPage.EnterProductDetails(product);
        homePage.PerformClickOnSpecialValue(product.Name, "Details");

        var actualProduct = productPage.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }
}