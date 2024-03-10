using Microsoft.Extensions.DependencyInjection;
using TestFramework.Driver;
using TestFramework.Pages;
using TestFramework.Extensions;

namespace TestProject;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.UseWebDriverInitializer();
        services.AddScoped<IDriverFixture, DriverFixture>();
        services.AddScoped<IBrowserDriver, BrowserDriver>();
        services.AddScoped<IHomePage, HomePage>();
        services.AddScoped<IProductPage, ProductPage>();
    }
}