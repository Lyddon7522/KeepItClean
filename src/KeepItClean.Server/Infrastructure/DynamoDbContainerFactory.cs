using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace KeepItClean.Server.Infrastructure;

public class DynamoDbContainerFactory : IAsyncDisposable
{
    private static IContainer _dynamoDbContainer = null!;

    private DynamoDbContainerFactory() { }

    public static async Task<string> CreateAsync()
    {
        _dynamoDbContainer = new ContainerBuilder()
            .WithImage("amazon/dynamodb-local:latest")
            .WithPortBinding(80, true)
            .Build();

        await _dynamoDbContainer.StartAsync();

        return "http://dynamodb-local:8000";
    }

    public ValueTask DisposeAsync() => _dynamoDbContainer.DisposeAsync();
}
