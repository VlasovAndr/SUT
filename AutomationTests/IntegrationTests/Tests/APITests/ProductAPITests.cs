using FluentAssertions;
using IntegrationTests.WebAppFactories;
using Newtonsoft.Json;
using ProductAPI.Data;
using System.Net;
using System.Text;


namespace IntegrationTests.Tests.APITests;

[Trait("Category", "APITests")]
public class ProductAPITests : ProductAPITestBase
{
    private readonly HttpClient _client;

    public ProductAPITests(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory) : base(webApplicationFactory)
    {
        _client = webApplicationFactory.CreateClient();
    }

    private async Task<Product> CreateProductThroughAPI()
    {
        var productData = GetProduct();
        string jsonValue = JsonConvert.SerializeObject(productData);
        StringContent content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("Product/Create", content);
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        var product = JsonConvert.DeserializeObject<Product>(apiResponse.Result.ToString());

        return product;
    }

    [Fact]
    public async Task GetProductsTest()
    {
        // Act
        var response = await _client.GetAsync("Product/GetProducts");
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Message.Should().BeNullOrEmpty();
        apiResponse.Result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProductByIdTest()
    {
        // Arrange
        var createdProduct = await CreateProductThroughAPI();

        // Act
        var response = await _client.GetAsync($"Product/GetProductById/{createdProduct.Id}");
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        var product = JsonConvert.DeserializeObject<Product>(apiResponse.Result.ToString());
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Message.Should().BeNullOrEmpty();
        apiResponse.Result.Should().NotBeNull();
        product.Should().BeEquivalentTo(createdProduct, option => option.Excluding(p => p.Id));
    }

    [Fact]
    public async Task GetProductByNonExistentIdTest()
    {
        // Act
        var response = await _client.GetAsync("Product/GetProductById/9999");
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        apiResponse.Result.Should().BeNull();
        apiResponse.IsSuccess.Should().BeFalse();
        apiResponse.Message.Should().Contain("Not found. The requested resource could not be found on the server.");
    }

    [Fact]
    public async Task CreateProductTest()
    {
        // Arrange
        var expectedProduct = GetProduct();
        string jsonValue = JsonConvert.SerializeObject(expectedProduct);
        StringContent content = new StringContent(jsonValue, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("Product/Create", content);
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        var product = JsonConvert.DeserializeObject<Product>(apiResponse.Result.ToString());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Message.Should().BeNullOrEmpty();
        product.Should().BeEquivalentTo(expectedProduct, option => option.Excluding(p => p.Id));
    }

    [Fact]
    public async Task UpdateProductTest()
    {
        // Arrange
        var createdProduct = await CreateProductThroughAPI();
        var updatedProduct = GetProduct();
        updatedProduct.Id = createdProduct.Id;
        
        // Act
        string jsonValue = JsonConvert.SerializeObject(updatedProduct);
        StringContent content = new StringContent(jsonValue, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync("Product/Update", content);
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);
        var product = JsonConvert.DeserializeObject<Product>(apiResponse.Result.ToString());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Message.Should().BeNullOrEmpty();
        product.Should().BeEquivalentTo(updatedProduct, option => option.Excluding(p => p.Id));
    }

    [Fact]
    public async Task DeleteProductTest()
    {
        // Arrange
        var createdProduct = await CreateProductThroughAPI();

        // Act
        var response = await _client.DeleteAsync($"Product/Delete?id={createdProduct.Id}");
        var result = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Message.Should().BeNullOrEmpty();
    }

}
