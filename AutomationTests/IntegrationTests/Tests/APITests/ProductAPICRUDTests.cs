using FluentAssertions;
using IntegrationTests.WebAppFactories;
using Newtonsoft.Json;
using ProductAPI.Data;


namespace IntegrationTests.Tests.APITests;

[Trait("Category", "APITests")]
public class ProductAPICRUDTests : ProductAPITestBase
{
    private readonly ProductAPIClient _productClient;

    public ProductAPICRUDTests(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory) : base(webApplicationFactory)
    {
        _productClient = GetProductAPIClient(webApplicationFactory);
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProductAfterCreation()
    {
        // Arrange
        var product = GetProduct();
        var response = await _productClient.CreateAsync(product);
        var createdProduct = JsonConvert.DeserializeObject<Product>(response.Result.ToString());

        // Act
        var resp = await _productClient.GetProductByIdAsync(createdProduct.Id);
        var productFromAPI = JsonConvert.DeserializeObject<Product>(resp.Result.ToString());

        //// Assert
        productFromAPI.Should().NotBeNull();
        productFromAPI.Should().BeEquivalentTo(createdProduct);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnProductsAfterCreation()
    {
        // Arrange
        var products = GetProducts();

        foreach (var p in products)
        {
            await _productClient.CreateAsync(p);
        }

        // Act
        var response = await _productClient.GetProductsAsync();
        var productsFromAPI = JsonConvert.DeserializeObject<List<Product>>(response.Result.ToString());

        // Assert
        productsFromAPI.Should().NotBeNull();
        productsFromAPI.Should().HaveCountGreaterThanOrEqualTo(products.Count);
        productsFromAPI.Select(c => c.Name).Should().Contain(products.Select(c => c.Name));
    }

    [Fact]
    public async Task Update_ShouldChangeProduct()
    {
        // Arrange
        var product = GetProduct();
        var response = await _productClient.CreateAsync(product);
        var createdProduct = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
        createdProduct.Name = "newName";
        createdProduct.Price = 999;

        // Act
        var updatedProduct = await _productClient.UpdateAsync(createdProduct);
        var productNew = await _productClient.GetProductByIdAsync(createdProduct.Id);

        // Assert
        productNew.Should().NotBeNull();
        productNew.Should().BeEquivalentTo(updatedProduct);
    }

    [Fact]
    public async Task Delete_ShouldRemoveProduct()
    {
        // Arrange
        var product = GetProduct();
        var response = await _productClient.CreateAsync(product);
        var createdProduct = JsonConvert.DeserializeObject<Product>(response.Result.ToString());

        // Act
        await _productClient.DeleteAsync(createdProduct.Id);
        var resp = await _productClient.GetProductsAsync();
        var productsFromAPI = JsonConvert.DeserializeObject<List<Product>>(resp.Result.ToString());

        // Assert
        productsFromAPI.Should().NotContain(p => p.Id == createdProduct.Id);
    }

    [Fact]
    public async Task CRUD_Test()
    {
        // Create
        var product = GetProduct();
        var createResponse = await _productClient.CreateAsync(product);
        var createdProduct = JsonConvert.DeserializeObject<Product>(createResponse.Result.ToString());
        
        // Update
        createdProduct.Name = "newName";
        createdProduct.Price = 999;
        var updateResponse = await _productClient.UpdateAsync(createdProduct);
        var updatedProduct = JsonConvert.DeserializeObject<Product>(updateResponse.Result.ToString());

        // Delete
        await _productClient.DeleteAsync(updatedProduct.Id);
        
        // Read
        var getProductsResponse = await _productClient.GetProductsAsync();
        var productsFromAPI = JsonConvert.DeserializeObject<List<Product>>(getProductsResponse.Result.ToString());

        // Assert
        productsFromAPI.Should().NotContain(p => p.Id == createdProduct.Id);
    }

}
