using OpenQA.Selenium;
using ProductAPI.Data;
using TestFramework.Driver;
using TestFramework.Pages;

namespace TestProject
{
    public class UnitTest1 : IDisposable
    {
        private readonly IHomePage homePage;
        private readonly ICreateProductPage createProductPage;
        private readonly IWebDriver driver;

        public UnitTest1(IDriverFixture driverFixture, IHomePage homePage, ICreateProductPage createProductPage)
        {
            driver = driverFixture.Driver;
            driver.Navigate().GoToUrl("http://localhost:5087/");
            this.homePage = homePage;
            this.createProductPage = createProductPage;
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Test1()
        {
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