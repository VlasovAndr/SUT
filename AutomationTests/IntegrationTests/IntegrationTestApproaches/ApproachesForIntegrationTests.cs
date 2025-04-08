using FluentAssertions;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProductAPI.Data;
using System.Net;

namespace IntegrationTests.IntegrationTestApproaches;

public class ApproachesForIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly string _baseUrl;

    public ApproachesForIntegrationTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _baseUrl = ServicePathHelper.GetProductAPIUrl();
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
        client.BaseAddress = new Uri(_baseUrl);

        var response = client.Send(new HttpRequestMessage(HttpMethod.Get, "Product/GetProducts"));

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = response.Content.ReadAsStringAsync().Result;
        result.Should().Contain("Intel Core i9");
        result.Should().Contain("\"isSuccess\":true");
        result.Should().Contain("\"message\":\"\"");
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

        var response = await webClient.GetAsync("Product/GetProducts");

        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = response.Content.ReadAsStringAsync().Result;
        result.Should().Contain("Intel Core i9");
        result.Should().Contain("\"isSuccess\":true");
        result.Should().Contain("\"message\":\"\"");
    }

    /// <summary>
    /// Problem with this approach is:
    /// 1. You need to have Database running (API is running in memory)
    /// </summary>
    [Fact]
    public async Task TestWithWebAppFactoryAndGeneratedCode()
    {
        var webClient = _webApplicationFactory.CreateClient();
        var productClient = new ProductAPIClient(_baseUrl, webClient);

        var response = await productClient.GetProductsAsync();

        // Status code is checked inside auto-generated GetProductsAsync method
        response.IsSuccess.Should().BeTrue();
        response.Message.Should().BeNullOrEmpty();
        response.Result.Should().NotBeNull();
        var products = JsonConvert.DeserializeObject<List<Product>>(response.Result.ToString());
        products.Should().HaveCountGreaterThan(1);
    }
}