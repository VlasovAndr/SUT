using FluentAssertions;
using IntegrationTest.Helpers;

namespace IntegrationTest.IntegrationTestApproaches;

public class IntegrationTestWithContainers : IClassFixture<CustomWebApplicationFactoryWithContainers<Program>>
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public IntegrationTestWithContainers(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory)
    {
        _client = webApplicationFactory.CreateClient();
        _baseUrl = ServicePathHelper.GetProductAPIUrl();
    }

    /// <summary>
    /// Problem with this approach is:
    /// 1. You need to have enviroment with Docker
    /// Advantages:
    /// 1. API is running in memory
    /// 2. Database is running in Docker container. It is close to production db
    /// 3. Using NSwag you can easily generate an API client(base on swagger.json) with all the necessary methods
    /// </summary>
    [Fact]
    public async Task TestWithCustomWebAppFactoryAndGeneratedCode()
    {
        var productClient = new ProductAPI(_baseUrl, _client);

        var products = await productClient.GetProductsAsync();

        products.Should().HaveCount(5);
    }

}