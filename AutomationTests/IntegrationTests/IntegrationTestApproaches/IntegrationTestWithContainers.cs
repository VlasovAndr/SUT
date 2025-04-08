using FluentAssertions;
using IntegrationTests.Helpers;
using IntegrationTests.WebAppFactories;
using Newtonsoft.Json;
using ProductAPI.Data;

namespace IntegrationTests.IntegrationTestApproaches;

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
        var productClient = new ProductAPIClient(_baseUrl, _client);

        var response = await productClient.GetProductsAsync();

        response.IsSuccess.Should().BeTrue();
        response.Message.Should().BeNullOrEmpty();
        response.Result.Should().NotBeNull();
        var products = JsonConvert.DeserializeObject<List<Product>>(response.Result.ToString());
        products.Should().HaveCountGreaterThan(1);
    }

}