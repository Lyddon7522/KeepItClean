using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using KeepItClean.Server.Domain;
using KeepItClean.Server.Infrastructure;
using KeepItClean.Shared.Features;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add swagger.
if (builder.Environment.IsDevelopment())
{
    // TODO: AWS, Firebase secrets.
    builder.Configuration.AddUserSecrets<Program>();
}

if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Test"))
{
    var connectionString = await DynamoDbContainerFactory.CreateAsync();

    builder.Configuration.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    {
        new KeyValuePair<string, string?>("DynamoDb:LocalServiceUrl", connectionString)
    });
}
else
{
}

builder.Services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(new AmazonDynamoDBConfig { ServiceURL = builder.Configuration.GetValue<string>("DynamoDb:LocalServiceUrl") }));
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

// TODO: Wire up docker (linux)
// TODO: DynamoDB local (docker) -> https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.html
// TODO: DynamoDB health checks.

var app = builder.Build();

app.UseHttpsRedirection();

// TODO: Auth.
// TODO: automapper.
// TODO: test.
app.MapPost("/api/locations", async (IRepository<Location> repository, AddLocationRequest request, CancellationToken cancellationToken) =>
{
    var location = new Location { Coordinates = request.Coordinates, Name = request.Name };
    await repository.AddAsync(location, cancellationToken);
});

app.Run();


public partial class Program { }