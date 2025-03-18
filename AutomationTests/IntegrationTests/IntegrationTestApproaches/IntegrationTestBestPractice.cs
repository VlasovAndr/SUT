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
    /// 2. Using NSwag you can easily generate an API client(base on swagger.json) with all the necessary methods
    /// </summary>
    [Fact]
    public async Task TestWithCustomWebAppFactoryAndGeneratedCode()
    {
        var product = new ProductAPI(_baseUrl, _client);

        var results = await product.GetProductsAsync();

        results.Should().HaveCount(5);
    }
}