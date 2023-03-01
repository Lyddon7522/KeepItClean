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
    private static string _connectionString = null!;

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        // TODO database.
        //_connectionString = await MsSqlContainerFactory.CreateAsync();

        _application = CreateApplication();

        _scope = _application.Services.CreateAsyncScope();
    }

    public static async Task ResetStateAsync()
    {
    }

    internal static WebApplicationFactory<Program> GetUnauthenticatedApplication()
        => _application;

    private static WebApplicationFactory<Program> CreateApplication()
    {
        var application = new WebApplicationFactory<Program>();

        return application;
    }
}
