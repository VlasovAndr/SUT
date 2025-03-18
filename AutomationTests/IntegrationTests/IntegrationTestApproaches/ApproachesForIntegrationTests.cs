using FluentAssertions;
using IntegrationTest.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTest.IntegrationTestApproaches;

public class ApproachesForIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public ApproachesForIntegrationTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    /// <summary>
    /// Problem with this approach is:
    /// 1. You need to have application running (API + Database)
    /// 2. Hardcoded request and endpoint paths
    /// 3. Hard to maintain in case of changes
    /// </summary>
    [Fact]
    public void TestWithHttpClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(ServicePathHelper.GetProductAPIUrl());

        var responce = client.Send(new HttpRequestMessage(HttpMethod.Get, "Product/GetProducts"));

        responce.EnsureSuccessStatusCode();
        var result = responce.Content.ReadAsStringAsync().Result;
        result.Should().Contain("Intel Core i9");
    }

    /// <summary>
    /// Problem with this approach is:
    /// 1. You need to have Database running (API is running in memory)
    /// 2. Hardcoded endpoint paths
    /// 3. Hard to maintain in case of changes
    /// </summary>
    [Fact]
    public async Task TestWithWebAppFactory()
    {
        var webClient = _webApplicationFactory.CreateClient();

        var product = await webClient.GetAsync("Product/GetProducts");
        var result = product.Content.ReadAsStringAsync().Result;

        result.Should().Contain("Intel Core i9");
    }

    /// <summary>
    /// Problem with this approach is:
    /// 1. You need to have Database running (API is running in memory)
    /// </summary>
    [Fact]
    public async Task TestWithWebAppFactoryAndGeneratedCode()
    {
        var webClient = _webApplicationFactory.CreateClient();
        var productClient = new ProductAPI(ServicePathHelper.GetProductAPIUrl(), webClient);

        var products = await productClient.GetProductsAsync();

        products.Should().HaveCount(5);
    }
}