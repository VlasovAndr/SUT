using Newtonsoft.Json;

namespace WebApp.Producer;

public class ProductService : IProductService
{
    private readonly ProductAPI _productApiClient;

    public ProductService() => _productApiClient = new ProductAPI("http://eaapi:8001", new HttpClient());

    public async Task<Product> CreateProduct(Product product)
    {
        var response = await _productApiClient.CreateAsync(product);
        var productFromResponse = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
        return productFromResponse;
    }

    public async Task DeleteProduct(int id)
    {
        await _productApiClient.DeleteAsync(id);
    }

    public async Task<Product> EditProduct(Product product)
    {
        var response = await _productApiClient.UpdateAsync(product);
        var productFromResponse = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
        return productFromResponse;
    }

    public async Task<ICollection<Product>> GetProducts()
    {
        var response = await _productApiClient.GetProductsAsync();
        var products = JsonConvert.DeserializeObject<List<Product>>(response.Result.ToString());
        return products;
    }

    public async Task<Product> GetProductById(int Id)
    {
        var response = await _productApiClient.GetProductByIdAsync(Id);
        var productFromResponse = JsonConvert.DeserializeObject<Product>(response.Result.ToString());
        return productFromResponse;
    }
}
