using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KeepItClean.Server.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeepItClean.Web.Tests;

public abstract class IntegrationTestBase
{
    [SetUp]
    protected async Task InitializeAsync()
    {
        try
        {
            var dynamoDbClient = GetRequiredService<IAmazonDynamoDB>();
            await dynamoDbClient.DeleteTableAsync("Locations");
        }
        catch (ResourceNotFoundException) { }

        var databaseInitializer = GetRequiredService<InitializeDatabaseService>();
        await databaseInitializer.InitializeAsync(CancellationToken.None);
    }

    protected static T GetRequiredService<T>() where T : class =>
        _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<T>();

    protected static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        await context.AddAsync(entity);
    }

    protected static async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Func<TEntity, bool>? predicate = null)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        TEntity? result;
        if (predicate is null)
        {
            result = (await context.GetAllAsync([])).FirstOrDefault();
        }
        else
        {
            result = (await context.GetAllAsync([])).FirstOrDefault(predicate);
        }

        return result;
    }

    protected static async Task SendAsync(IRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(request);
    }

    protected static async Task<TResponse?> SendAsync<TResponse>(IRequest<TResponse?> request)
        where TResponse : class
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(request);
    }

    private static readonly IServiceScopeFactory _scopeFactory = null!;

    static IntegrationTestBase()
    {
        var connectionString = DynamoDbContainerFactory.CreateAsync().Result;

        var webApplicationBuilder = WebApplication.CreateBuilder();
        webApplicationBuilder.Configuration.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
        {
            new("AWS:ServiceUrl", connectionString),
            new("AWS:AccessKeyId", "TestUser"),
            new("AWS:SecretAccessKey", "TestSecret")
        });

        webApplicationBuilder.AddApplicationServices();

        _scopeFactory = webApplicationBuilder.Build().Services.GetRequiredService<IServiceScopeFactory>();

        SetupDatabaseAsync().GetAwaiter().GetResult();
    }

    private static async Task SetupDatabaseAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var initializeDatabaseService = scope.ServiceProvider.GetRequiredService<InitializeDatabaseService>();

        await initializeDatabaseService.InitializeAsync(CancellationToken.None);
    }
}