using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using KeepItClean.Server.Domain;
using KeepItClean.Server.Infrastructure;
using KeepItClean.Shared.Features;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add swagger.
if (builder.Environment.IsEnvironment("Local"))
{
    // TODO: AWS, Firebase secrets.
    builder.Configuration.AddUserSecrets<Program>();
}

// TODO: revist this config
var credentials = new BasicAWSCredentials("<ACCESS_KEY>", "<SECRET_KEY>");
var config = new AmazonDynamoDBConfig()
{
    RegionEndpoint = RegionEndpoint.USEast1
};
var client = new AmazonDynamoDBClient(credentials, config);
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
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