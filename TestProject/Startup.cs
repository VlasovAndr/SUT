using TestFramework.Settings;
using Microsoft.Extensions.DependencyInjection;
using TestFramework.Driver;
using TestFramework.Pages;

namespace TestProject;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDriverFixture, DriverFixture>();
        services.AddScoped<IBrowserDriver, BrowserDriver>();
        services.AddSingleton(new TestSettings
        {
            BrowserType = BrowserType.Chrome
        });
        services.AddScoped<IHomePage, HomePage>();
        services.AddScoped<ICreateProductPage, CreateProductPage>();

    }
}