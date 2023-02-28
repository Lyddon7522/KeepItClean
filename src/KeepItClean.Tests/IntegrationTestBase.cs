using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace KeepItClean.Tests;

public class IntegrationTestBase
{
    [SetUp]
    public async Task InitializeAsync() => await ResetStateAsync();
}

[SetUpFixture]
public class IntegrationTesting
{
    private static Respawner _respawner = null!;
    private static WebApplicationFactory<Program> _application = null!;
    private static IServiceScope _scope = null!;
    private static string _connectionString = null!;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        // TODO database.
        //_connectionString = await MsSqlContainerFactory.CreateAsync();

        //_respawner = new Respawner
        //{
        //    TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
        //};

        _application = CreateApplication();

        _scope = _application.Services.CreateAsyncScope();

        //var dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //await dbContext.Database.EnsureDeletedAsync();
        //await dbContext.Database.MigrateAsync();
    }

    public static async Task ResetStateAsync()
    {
        //await _respawner.ResetAsync(_connectionString);
    }

    internal static WebApplicationFactory<Program> GetUnauthenticatedApplication()
        => _application;

    private static WebApplicationFactory<Program> CreateApplication()
    {
        var application = new WebApplicationFactory<Program>();

        return application;
    }
}
