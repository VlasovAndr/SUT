using AutoFixture;
using IntegrationTests.Helpers;
using IntegrationTests.WebAppFactories;
using ProductAPI.Data;

namespace IntegrationTests.Tests;

public class ProductAPITestBase : IClassFixture<CustomWebApplicationFactoryWithContainers<Program>>
{
    protected CustomWebApplicationFactoryWithContainers<Program> WebApplicationFactory { get; }

    public ProductAPITestBase(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory)
    {
        WebApplicationFactory = webApplicationFactory;
    }

    protected Product GetProduct()
    {
        var fixture = new Fixture();
        fixture.Customize<Product>(c => c.Without(x => x.Id));
        return fixture.Create<Product>();
    }

    protected List<Product> GetProducts()
    {
        var fixture = new Fixture();
        fixture.Customize<Product>(c => c.Without(x => x.Id));
        return fixture.Create<List<Product>>();
    }

    protected ProductAPIClient GetProductAPIClient(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory)
    {
        var client = webApplicationFactory.CreateClient();
        var baseUrl = ServicePathHelper.GetProductAPIUrl();
        return new ProductAPIClient(baseUrl, client);
    }
}
