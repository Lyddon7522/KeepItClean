using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Net;

namespace KeepItClean.Server.Infrastructure;

public class InitializeDatabaseService
{
    private readonly AmazonDynamoDBClient _client;
    public InitializeDatabaseService(AmazonDynamoDBClient client)
    {
        _client = client;
    }

    public async Task InitializeAsync()
    {
        string tableName = "Locations";

        var request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                  AttributeName = "Id",
                  AttributeType = "N"
                }
            },
            KeySchema = new List<KeySchemaElement>()
            {
                new KeySchemaElement
                {
                    AttributeName = "Id",
                    KeyType = "HASH"  //Partition key
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 10,
                WriteCapacityUnits = 5
            }
        };

        var response = await _client.CreateTableAsync(request);

        // TODO: make sure this matches reality. https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetWorkingWithTables.html
        // TODO: what if its already initialized? 
        if (response.HttpStatusCode != HttpStatusCode.OK) throw new FailedToInitializeDatabaseException($"Database failed to initialize with status code of {response.HttpStatusCode}.");
    }
}

internal class FailedToInitializeDatabaseException : Exception
{
    public FailedToInitializeDatabaseException(string message) : base(message)
    {

    }
}