using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
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

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        _application = CreateApplication();
    }

    public static async Task ResetStateAsync()
    {
        try
        {
            var dynamoDbClient = _application.Services.GetRequiredService<IAmazonDynamoDB>();
            await dynamoDbClient.DeleteTableAsync("Locations");
        }
        catch (ResourceNotFoundException) { }

        var databaseInitializer = _application.Services.GetRequiredService<InitializeDatabaseService>();
        await databaseInitializer.InitializeAsync(CancellationToken.None);
    }

    internal static WebApplicationFactory<Program> GetUnauthenticatedApplication()
        => _application;

    private static WebApplicationFactory<Program> CreateApplication()
    {
        var application = new WebApplicationFactory<Program>();

        return application;
    }

    internal static async Task<TEntity?> FirstOrDefaultAsync<TEntity>() where TEntity : class
    {
        var repository = _application.Services.GetRequiredService<IRepository<TEntity>>();

        var entity = (await repository.GetAllAsync(Array.Empty<ScanCondition>(), CancellationToken.None)).FirstOrDefault();

        return entity;
    }
}
