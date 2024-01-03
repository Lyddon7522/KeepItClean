using Testcontainers.DynamoDb;

namespace KeepItClean.Server.Infrastructure;

public sealed class DynamoDbContainerFactory : IAsyncDisposable
{
    private static DynamoDbContainer _dynamoDbContainer = null!;

    private DynamoDbContainerFactory() { }

    public static async Task<string> CreateAsync()
    {
        _dynamoDbContainer = new DynamoDbBuilder().Build();

        await _dynamoDbContainer.StartAsync();

        return _dynamoDbContainer.GetConnectionString();
    }

    public ValueTask DisposeAsync() => _dynamoDbContainer.DisposeAsync();
}
