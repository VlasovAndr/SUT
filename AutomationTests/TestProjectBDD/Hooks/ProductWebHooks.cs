using Allure.Net.Commons;
using System.Text;
using Reqnroll;
using TestFramework.Driver;
using TestProjectBDD.StepDefinitions;
using TestFramework.Reports;

namespace TestProjectBDD.Hooks;

[Binding]
public class ProductWebHooks : BaseHooks
{
    private readonly ProductWebSteps productSteps;
    private readonly IDriverFixture driver;


    public ProductWebHooks(ScenarioContext scenarioContext, ProductWebSteps productSteps, IDriverFixture driver) : base(scenarioContext)
    {
        this.productSteps = productSteps;
        this.driver = driver;
    }

    #region Setups

    [BeforeScenario("@Setup.UI.CreateProduct")]
    public void CreateProduct()
    {
        var productName = GetParameterFromTag("ProductName");
        var description = GetParameterFromTag("Description");
        var price = GetParameterFromTag("Price");
        var productType = GetParameterFromTag("ProductType");
        var productTable = new Table("Name", "Description", "Price", "ProductType");
        productTable.AddRow(productName, description, price, productType);

        productSteps.CreateProductWithFollowingDetails(productTable);
    }

    #endregion


    #region Teardowns

    [AfterScenario("@UI_Steps")]
    public void AfterEach()
    {
        if (scenarioContext.TestError != null)
        {
            var screenshot = driver.GetScreenshot();
            var browserLogs = driver.GetBrowserLogs();
            var pageSource = driver.Driver.PageSource;
            var reporter = new AllureReporter();
            reporter.AddAttachment("errorScreenshot.png", "image/png", screenshot);
            reporter.AddAttachment("browserLogs.txt", "text/plain", Encoding.ASCII.GetBytes(browserLogs));
            reporter.AddAttachment("pageSource.html", "text/html", Encoding.ASCII.GetBytes(pageSource));
        }
    }

    [AfterScenario("@Teardown.UI.DeleteCreatedProduct")]
    public void DeleteProduct()
    {
        productSteps.DeleteProduct();
    }

    #endregion
}
