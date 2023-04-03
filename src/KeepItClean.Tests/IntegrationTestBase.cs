using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace KeepItClean.Tests;

public class IntegrationTestBase
{
    [SetUp]
    public async Task InitializeAsync() => await ResetStateAsync();
}

[SetUpFixture]
public class IntegrationTesting
{
    private static WebApplicationFactory<Program> _application = null!;
    private static IServiceScope _scope = null!;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        _application = CreateApplication();

        _scope = _application.Services.CreateAsyncScope();
    }

    public static async Task ResetStateAsync()
    {
        var client = _scope.ServiceProvider.GetRequiredService<IAmazonDynamoDB>();

        await client.DeleteTableAsync("Locations");
    }

    internal static WebApplicationFactory<Program> GetUnauthenticatedApplication()
        => _application;

    private static WebApplicationFactory<Program> CreateApplication()
    {
        var application = new WebApplicationFactory<Program>();

        return application;
    }
}
