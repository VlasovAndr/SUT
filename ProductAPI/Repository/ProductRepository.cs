using ProductAPI.Data;

namespace ProductAPI.Repository;

public interface IProductRepository
{
    Product AddProduct(Product product);
    void DeleteProduct(int id);
    void DeleteProduct(string productName);
    List<Product> GetAllProducts();
    Product GetProductById(int id);
    Product GetProductByName(string name);
    Product UpdateProduct(Product product);
}

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext context;

    public ProductRepository(ProductDbContext context)
    {
        this.context = context;
    }

    public List<Product> GetAllProducts()
    {
        return context.Products.ToList();
    }

    public Product GetProductById(int id)
    {
        return context.Products.FirstOrDefault(p => p.Id == id);
    }

    public Product AddProduct(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return product;
    }

    public Product UpdateProduct(Product product)
    {
        context.Products.Update(product);
        context.SaveChanges();
        return product;
    }

    public void DeleteProduct(int id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        context.Products.Remove(product);
        context.SaveChanges();
    }

    public void DeleteProduct(string productName)
    {
        var product = context.Products.FirstOrDefault(p => p.Name == productName);
        context.Products.Remove(product);
        context.SaveChanges();
    }

    public Product GetProductByName(string name)
    {
        return context.Products.FirstOrDefault(p => p.Name == name);
    }
}