using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KeepItClean.Server.Infrastructure.Models;
using System.Net;

namespace KeepItClean.Server.Infrastructure;

public class InitializeDatabaseService
{
    private readonly IAmazonDynamoDB _amazonDynamoDb;
    private const string _tableName = "Locations";

    public InitializeDatabaseService(IAmazonDynamoDB amazonDynamoDb)
    {
        _amazonDynamoDb = amazonDynamoDb;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (await LocationTableAlreadyExistsAsync(cancellationToken)) return;

        var request = new CreateTableRequest
        {
            TableName = _tableName,
            AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                  AttributeName = nameof(Location.Id),
                  AttributeType = "S"
                }
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = nameof(Location.Id),
                    KeyType = "HASH"  //Partition key - probably should be US state?
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 10,
                WriteCapacityUnits = 5
            }
        };

        var response = await _amazonDynamoDb.CreateTableAsync(request, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException($"Creating the dynamo db table was not successful. Table: {response.TableDescription.TableName}");
        }
    }

    private async Task<bool> LocationTableAlreadyExistsAsync(CancellationToken cancellationToken)
    {
        DescribeTableResponse? existingTable = null;
        try
        {
            existingTable = await _amazonDynamoDb.DescribeTableAsync(_tableName, cancellationToken);
        }
        catch (ResourceNotFoundException) { }
        if (existingTable is not null) return true;

        return false;
    }
}