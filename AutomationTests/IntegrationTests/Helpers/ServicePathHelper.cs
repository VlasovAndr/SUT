namespace IntegrationTests.Helpers;

public class ServicePathHelper
{
    public static string GetProductAPIUrl()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return environmentName == null ? "http://localhost:5000" : "http://eaapi:8001";
    }
}
