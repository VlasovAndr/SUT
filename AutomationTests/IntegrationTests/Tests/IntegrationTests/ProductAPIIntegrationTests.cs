using FluentAssertions;
using IntegrationTests.WebAppFactories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductAPI.Data;
using ProductAPI.Repository;


namespace IntegrationTests.Tests.IntegrationTests;

[Trait("Category", "IntegrationTests")]
public class ProductAPIIntegrationTests : ProductAPITestBase
{
    private readonly ProductAPIClient _productClient;
    private readonly IProductRepository _productRepository;

    public ProductAPIIntegrationTests(CustomWebApplicationFactoryWithContainers<Program> webApplicationFactory) : base(webApplicationFactory)
    {
        _productClient = GetProductAPIClient(webApplicationFactory);
        _productRepository = WebApplicationFactory.Services.CreateScope().ServiceProvider.GetRequiredService<IProductRepository>();
    }

    [Fact]
    public async Task GetProductById_ShouldReturnProductFromDatabase()
    {
        // Arrange
        var product = GetProduct();
        var productFromDb = _productRepository.AddProduct(product);

        // Act
        var response = await _productClient.GetProductByIdAsync(productFromDb.Id);
        var productFromAPI = JsonConvert.DeserializeObject<Product>(response.Result.ToString());

        //// Assert
        productFromAPI.Should().NotBeNull();
        productFromAPI.Should().BeEquivalentTo(productFromDb);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnProductsFromDatabase()
    {
        // Arrange
        var products = GetProducts();

        products.ForEach(p => _productRepository.AddProduct(p));

        // Act
        var response = await _productClient.GetProductsAsync();
        var productsFromAPI = JsonConvert.DeserializeObject<List<Product>>(response.Result.ToString());

        // Assert
        productsFromAPI.Should().NotBeNull();
        productsFromAPI.Should().HaveCountGreaterThanOrEqualTo(products.Count);
        productsFromAPI.Select(c => c.Name).Should().Contain(products.Select(c => c.Name));
    }

    [Fact]
    public async Task Create_ShouldAddProductToDatabase()
    {
        // Arrange
        var product = GetProduct();

        // Act
        var response = await _productClient.CreateAsync(product);
        var createdProductFromAPI = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
        var productFromDb = _productRepository.GetProductById(createdProductFromAPI.Id);

        // Assert
        productFromDb.Should().NotBeNull();
        productFromDb.Should().BeEquivalentTo(createdProductFromAPI);
    }

    [Fact]
    public async Task Update_ShouldChangeProductInDatabase()
    {
        // Arrange
        var product = GetProduct();
        _productRepository.AddProduct(product);

        var productId = _productRepository.GetProductByName(product.Name).Id;
        product.Id = productId;

        product.Name = "newName";
        product.Price = 999;

        // Act

        var response = await _productClient.UpdateAsync(product);
        var updatedProduct = JsonConvert.DeserializeObject<Product>(response.Result.ToString());

        var productNew = _productRepository.GetProductById(updatedProduct.Id);

        // Assert
        productNew.Should().NotBeNull();
        productNew.Should().BeEquivalentTo(updatedProduct);
    }

    [Fact]
    public async Task Delete_ShouldExcludeProductFromDatabase()
    {
        // Arrange
        var product = GetProduct();
        var productFromDb = _productRepository.AddProduct(product);

        // Act
        await _productClient.DeleteAsync(productFromDb.Id);
        var removedProductFromDb = _productRepository.GetProductById(productFromDb.Id);
        var productsFromDb = _productRepository.GetAllProducts();

        // Assert
        removedProductFromDb.Should().BeNull();
        productsFromDb.Should().NotContain(p => p.Id == productFromDb.Id);
    }

}
