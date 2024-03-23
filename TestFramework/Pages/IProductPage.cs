using ProductAPI.Data;

namespace TestFramework.Pages;

public interface IProductPage
{
    void EnterProductDetails(Product product);
    Product GetProductDetails();
    void EditProduct(Product product);
}