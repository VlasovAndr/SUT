using FluentAssertions;
using IntegrationTest.Helpers;

namespace IntegrationTest.IntegrationTestApproaches;

public class IntegrationTestBestPractice : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> webApplicationFactory;

    public IntegrationTestBestPractice(CustomWebApplicationFactory<Program> webApplicationFactory)
    {
        this.webApplicationFactory = webApplicationFactory;
    }

    /// <summary>
    /// Advantages:
    /// 1. Database and API are running in memory
    /// 2. Using NSwag you can easily generate an API client(base on swagger.json) with all the necessary methods
    /// </summary>
    [Fact]
    public async Task TestWithCustomWebAppFactoryAndGeneratedCode()
    {
        var webClient = webApplicationFactory.CreateClient();
        var product = new ProductAPI(ServicePathHelper.GetProductAPIUrl(), webClient);

        var results = await product.GetProductsAsync();
        
        results.Should().HaveCount(5);
    }
}