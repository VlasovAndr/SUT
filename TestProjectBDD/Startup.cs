using TestFramework.Driver;
using TestFramework.Pages;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using TestFramework.Settings;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Repository;

namespace TestProjectBDD;

public static class Startup
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
        
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        environmentName = environmentName ?? "local";
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(connectionString));
        services.AddTransient<IProductRepository, ProductRepository>();

        services.AddSingleton(ReadConfig());
        services.AddScoped<IDriverFixture, DriverFixture>();
        services.AddScoped<IBrowserDriver, BrowserDriver>();
        services.AddScoped<IHomePage, HomePage>();
        services.AddScoped<IProductPage, ProductPage>();
        return services;
    }

    private static TestSettings ReadConfig()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        environmentName = environmentName == null ? "local" : environmentName;

        var configFile = File
                        .ReadAllText(Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location)
                        + $"/appsettings.{environmentName}.json");

        var jsonSerializeOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        jsonSerializeOptions.Converters.Add(new JsonStringEnumConverter());

        var testSettings = JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializeOptions);

        return testSettings;
    }
}