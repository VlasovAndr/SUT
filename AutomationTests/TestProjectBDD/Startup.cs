using TestFramework.Driver;
using TestFramework.Pages;
using System;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Repository;
using WebApp.Producer;
using TestFramework.Extensions;
using TestFramework.Pages.Locators;
using TestFramework.Driver.WebDriverFactory;

namespace TestProjectBDD;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseSqlServer(GetConnectionStringForDb());
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();

        });
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductService, ProductService>();

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

        return services;
    }

    private static string GetConnectionStringForDb()
    {
        string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];

        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        environmentName = environmentName ?? "local";
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        return connectionString;
    }

}