using Microsoft.Extensions.DependencyInjection;
using TestFramework.Driver;
using TestFramework.Pages;
using TestFramework.Extensions;

namespace TestProject;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTestSettings();
        services.AddScoped<IDriverFixture, DriverFixture>();
        services.AddScoped<IBrowserFactory, FirefoxDriverFactory>();
        services.AddScoped<IBrowserFactory, ChromeDriverFactory>();
        services.AddScoped<IBrowserFactory, RemoteChromeDriverFactory>();
        services.AddScoped<HomePage>();
        services.AddScoped<ProductPage>();
    }
}