using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KeepItClean.Server.Infrastructure;
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
        try
        {
            var dynamoDbClient = _scope.ServiceProvider.GetRequiredService<IAmazonDynamoDB>();
            await dynamoDbClient.DeleteTableAsync("Locations");
        }
        catch (ResourceNotFoundException) { }

        var databaseInitializer = _scope.ServiceProvider.GetRequiredService<InitializeDatabaseService>();
        await databaseInitializer.InitializeAsync(CancellationToken.None);
    }

    internal static WebApplicationFactory<Program> GetUnauthenticatedApplication()
        => _application;

    private static WebApplicationFactory<Program> CreateApplication()
    {
        var application = new WebApplicationFactory<Program>();

        return application;
    }
}
