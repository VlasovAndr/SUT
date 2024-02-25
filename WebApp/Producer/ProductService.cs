namespace WebApp.Producer;

public class ProductService : IProductService
{
	private readonly ProductAPI _productApiClient;

	public ProductService() => _productApiClient = new ProductAPI("https://localhost:7036", new HttpClient());

	public async Task<Product> CreateProduct(Product product)
	{
		return await _productApiClient.CreateAsync(product);
	}

	public async Task DeleteProduct(int id)
	{
		await _productApiClient.DeleteAsync(id);
	}

	public async Task<Product> EditProduct(Product product)
	{
		return await _productApiClient.UpdateAsync(product);
	}

	public async Task<ICollection<Product>> GetProducts()
	{
		return await _productApiClient.GetProductsAsync();
	}

	public async Task<Product> GetProductById(int Id)
	{
		return await _productApiClient.GetProductByIdAsync(Id);
	}
}
