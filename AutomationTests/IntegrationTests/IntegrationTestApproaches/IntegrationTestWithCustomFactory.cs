using FluentAssertions;
using IntegrationTests.Helpers;
using IntegrationTests.WebAppFactories;
using Newtonsoft.Json;
using ProductAPI.Data;

namespace IntegrationTests.IntegrationTestApproaches;

public class IntegrationTestWithCustomFactory : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public IntegrationTestWithCustomFactory(CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        _client = webApplicationFactory.CreateClient();
        _baseUrl = ServicePathHelper.GetProductAPIUrl();
    }

    /// <summary>
    /// Advantages:
    /// 1. Database and API are running in memory
    /// 2. Using NSwag you can easily generate an API client(based on swagger.json) with all the necessary methods
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