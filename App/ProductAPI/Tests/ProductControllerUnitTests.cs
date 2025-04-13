using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Data;
using ProductAPI.Errors;
using ProductAPI.Repository;
using Xunit;

namespace ProductAPI.Tests;

public class ProductControllerUnitTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductController _controller;

    public ProductControllerUnitTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _controller = new ProductController(_mockRepo.Object);
    }

    [Fact]
    public void GetProductById_ReturnsOkResult_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var mockProduct = new Product { Id = productId, Name = "Test Product" };
        _mockRepo.Setup(repo => repo.GetProductById(productId)).Returns(mockProduct);

        // Act
        var result = _controller.GetProductById(productId).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(mockProduct, response.Result);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Message);
        _mockRepo.Verify(repo => repo.GetProductById(productId), Times.Once);
    }

    [Fact]
    public void GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 999;
        _mockRepo.Setup(repo => repo.GetProductById(productId)).Returns((Product)null);

        // Act
        var result = _controller.GetProductById(productId).Result;

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(notFoundResult.Value);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.False(response.IsSuccess);
        Assert.Equal("Not found. The requested resource could not be found on the server.", response.Message);
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithListOfProducts()
    {
        // Arrange
        var mockProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };
        _mockRepo.Setup(repo => repo.GetAllProducts()).Returns(mockProducts);

        // Act
        var result = _controller.GetProducts().Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(mockProducts, response.Result);
        Assert.Empty(response.Message);
        Assert.True(response.IsSuccess);
        _mockRepo.Verify(repo => repo.GetAllProducts(), Times.Once);
    }

    [Fact]
    public void Create_ReturnsCreatedAtActionResult_WhenProductIsCreated()
    {
        // Arrange
        var newProduct = new Product { Id = 1, Name = "New Product" };
        _mockRepo.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Returns(newProduct);

        // Act
        var result = _controller.Create(newProduct).Result;

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var response = Assert.IsType<ApiResponse>(createdAtActionResult.Value);
        Assert.Equal(201, createdAtActionResult.StatusCode);
        Assert.Equal(newProduct, response.Result);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Message);
        _mockRepo.Verify(repo => repo.AddProduct(newProduct), Times.Once);
    }

    [Fact]
    public void Create_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        var newProduct = new Product { Id = 1, Name = "New Product" };
        _mockRepo.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Throws(new Exception("Database error"));

        // Act
        var result = _controller.Create(newProduct).Result;

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Contains("Bad request. The server could not understand your request.", response.Message);
        Assert.Contains("Database error", response.Message);
        Assert.False(response.IsSuccess);
        _mockRepo.Verify(repo => repo.AddProduct(newProduct), Times.Once);
    }

    [Fact]
    public void Update_ReturnsOkResult_WhenProductIsUpdated()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Updated Product" };
        _mockRepo.Setup(repo => repo.UpdateProduct(It.IsAny<Product>())).Returns(product);

        // Act
        var result = _controller.Update(product).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(product, response.Result);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Message);
        _mockRepo.Verify(repo => repo.UpdateProduct(product), Times.Once);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        var newProduct = new Product { Id = 1, Name = "New Product" };
        _mockRepo.Setup(repo => repo.UpdateProduct(It.IsAny<Product>())).Throws(new Exception("Database error"));

        // Act
        var result = _controller.Update(newProduct).Result;

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Contains("Bad request. The server could not understand your request.", response.Message);
        Assert.Contains("Database error", response.Message);
        Assert.False(response.IsSuccess);
    }

    [Fact]
    public void Delete_ReturnsOkResult_WhenProductIsDeleted()
    {
        // Arrange
        var productId = 1;
        _mockRepo.Setup(repo => repo.DeleteProduct(productId)).Verifiable();

        // Act
        var result = _controller.Delete(productId).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(okResult.Value);
        Assert.Equal(200, okResult.StatusCode);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Message);
        _mockRepo.Verify(repo => repo.DeleteProduct(productId), Times.Once);
    }

    [Fact]
    public void Delete_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        var productId = 1;
        _mockRepo.Setup(repo => repo.DeleteProduct(productId)).Throws(new Exception("Delete error"));

        // Act
        var result = _controller.Delete(productId).Result;

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var response = Assert.IsType<ApiResponse>(badRequestResult.Value);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Contains("Bad request. The server could not understand your request.", response.Message);
        Assert.Contains("Delete error", response.Message);
        Assert.False(response.IsSuccess);
    }
}
