using OpenQA.Selenium;
using ProductAPI.Data;
using TestFramework.Driver;
using TestFramework.Pages;

namespace TestProject
{
    public class UnitTest1 : IDisposable
    {
        private readonly IDriverFixture driverFixture;
        private readonly IWebDriver driver;

        public UnitTest1(IDriverFixture driverFixture)
        {
            driver = driverFixture.Driver;
            driver.Navigate().GoToUrl("http://localhost:5087/");
            this.driverFixture = driverFixture;
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Test1()
        {
            HomePage homePage = new HomePage(driverFixture);
            CreateProductPage createProductPage = new CreateProductPage(driverFixture);

            homePage.CreateProduct();
            createProductPage.EnterProductDetails(new Product
            {
                Name = "AutoProduct",
                Description = "Description",
                Price = 110,
                ProductType = ProductType.PERIPHARALS
            });
        }
    }
}