using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using KeepItClean.Server.Domain;
using KeepItClean.Server.Infrastructure;
using KeepItClean.Shared.Features;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Test"))
{
    var connectionString = await DynamoDbContainerFactory.CreateAsync();

    builder.Configuration.AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    {
        new("AWS:ServiceUrl", connectionString),
        new("AWS:AccessKeyId", "TestUser"),
        new("AWS:SecretAccessKey", "TestSecret")
    });
}
else
{
}

builder.Services.AddDefaultAWSOptions(provider =>
{
    var options = builder.Configuration.GetAWSOptions();
    options.Credentials = new BasicAWSCredentials(builder.Configuration.GetValue<string>("AWS:AccessKeyId"), builder.Configuration.GetValue<string>("AWS:SecretAccessKey"));

    return options;
});
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<InitializeDatabaseService>();


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