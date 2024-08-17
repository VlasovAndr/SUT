using Microsoft.Extensions.DependencyInjection;
using TestFramework.Driver;
using TestFramework.Pages;
using TestFramework.Extensions;
using TestFramework.Pages.Locators;
using TestFramework.Driver.WebDriverFactory;

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

        services.AddScoped<Header>();
        services.AddScoped<ProductListingPage>();
        services.AddScoped<ProductDetailsPage>();

        services.AddSingleton<HeaderLocators>();
        services.AddSingleton<ProductListingLocators>();
        services.AddSingleton<ProductDetailsLocators>();
    }
}