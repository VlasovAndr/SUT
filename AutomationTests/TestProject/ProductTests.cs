using Xunit;
using AutoFixture.Xunit2;
using FluentAssertions;
using TestFramework.Pages;
using WebApp;

namespace TestProject;

public class ProductTests
{
    private readonly ProductListingPage productListing;
    private readonly ProductDetailsPage productDetails;

    public ProductTests(ProductListingPage productListing, ProductDetailsPage productDetails)
    {
        this.productListing = productListing;
        this.productDetails = productDetails;
    }

    [Theory, AutoData]
    public void CreateProductTest(Product product)
    {
        productListing.Header.OpenProductMenu();
        productListing.ClickCreateProduct();
        productDetails.FillProductFields(product);
        productDetails.ClickCreate();
        productListing.PerformClickOnSpecialValue(product.Name, "Details");

        var actualProduct = productDetails.GetProductDetails();

        actualProduct
            .Should()
            .BeEquivalentTo(product, option => option.Excluding(x => x.Id));
    }
}