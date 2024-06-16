using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using TestFramework.Settings;

namespace TestFramework.Extensions;

public static class WebDriverInitializerExtension
{
    public static IServiceCollection AddTestSettings(
        this IServiceCollection services)
    {
        services.AddSingleton(ReadConfig());

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