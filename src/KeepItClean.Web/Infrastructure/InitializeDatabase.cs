using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using KeepItClean.Server.Infrastructure.Models;
using System.Net;

namespace KeepItClean.Server.Infrastructure;

public class InitializeDatabaseService(IAmazonDynamoDB amazonDynamoDb)
{
    private const string _tableName = "Locations";

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (await LocationTableAlreadyExistsAsync(cancellationToken)) return;

        var request = new CreateTableRequest
        {
            TableName = _tableName,
            AttributeDefinitions =
            [
                new AttributeDefinition
                {
                    AttributeName = nameof(Location.Id),
                    AttributeType = "S"
                }
            ],
            KeySchema =
            [
                new KeySchemaElement
                {
                    AttributeName = nameof(Location.Id),
                    KeyType = "HASH"  //Partition key - probably should be US state?
                }
            ],
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 10,
                WriteCapacityUnits = 5
            }
        };

        var response = await amazonDynamoDb.CreateTableAsync(request, cancellationToken);

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
            existingTable = await amazonDynamoDb.DescribeTableAsync(_tableName, cancellationToken);
        }
        catch (ResourceNotFoundException) { }
        if (existingTable is not null) return true;

        return false;
    }
}