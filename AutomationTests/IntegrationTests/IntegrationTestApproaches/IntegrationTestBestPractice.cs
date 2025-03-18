using FluentAssertions;
using IntegrationTest.Helpers;

namespace IntegrationTest.IntegrationTestApproaches;

public class IntegrationTestBestPractice : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public IntegrationTestBestPractice(CustomWebApplicationFactory<Program> webApplicationFactory)
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
        var productClient = new ProductAPI(_baseUrl, _client);

        var products = await productClient.GetProductsAsync();

        products.Should().HaveCount(5);
    }
}