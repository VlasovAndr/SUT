using ProductAPI.Data;
using ProductAPI.Repository;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace TestProjectBDD.StepDefinitions
{
    [Binding]
    public class ProductDbSteps
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IProductRepository productRepository;

        public ProductDbSteps(ScenarioContext scenarioContext, IProductRepository productRepository)
        {
            this.scenarioContext = scenarioContext;
            this.productRepository = productRepository;
        }

        [Then(@"I delete the product (.*) for cleanup")]
        public void ThenIDeleteTheProductHeadphonesForCleanup(string productName)
        {
            productRepository.DeleteProduct(productName);
        }

        [Given(@"I ensure the following product is created")]
        public void GivenIEnsureTheFollowingProjectIsCreated(Table table)
        {
            var product = table.CreateInstance<Product>();
            productRepository.AddProduct(product);
            scenarioContext.Set(product);
        }

        [Given(@"I cleanup following data")]
        public void GivenICleanupFollowingData(Table table)
        {
            var products = table.CreateSet<Product>();

            foreach (var product in products)
            {
                var prod = productRepository.GetProductByName(product.Name);

                if (prod != null)
                    productRepository.DeleteProduct(product.Name);
            }
        }
    }
}
