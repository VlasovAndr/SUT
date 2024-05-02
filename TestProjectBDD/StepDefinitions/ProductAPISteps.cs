using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WebApp.Producer;

namespace TestProjectBDD.StepDefinitions
{
    [Binding, Scope(Tag = "API_Steps")]
    public class ProductAPISteps
    {
        private readonly IProductService productService;

        public ProductAPISteps(IProductService productService)
        {
            this.productService = productService;
        }

        [Given(@"I create product with following details")]
        public void CreateProductWithFollowingDetails(Table table)
        {
            var products = table.CreateSet<WebApp.Product>();

            foreach (var product in products)
            {
                productService.CreateProduct(product);
            }
        }
    }
}
